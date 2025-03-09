using AutoMapper;
using Entities.Auth_Models;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthencationManager : IAuthenticationService
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _con;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthencationManager> _logger;

        private User? _user;

        public AuthencationManager(UserManager<User> userManager, IConfiguration con, IMapper mapper)
        {
            _userManager = userManager;
            _con = con;
            _mapper = mapper;
        }

        public async Task<tokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSigninCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if (populateExp)
            {
                _user.ResfreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }

            await _userManager.UpdateAsync(_user);

            await _userManager.SetAuthenticationTokenAsync(_user, "JWT", "RefreshToken", refreshToken);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new tokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> regUser(UserF_Reg userF_Reg)
        {
            var user = _mapper.Map<User>(userF_Reg);

            var result = await _userManager.CreateAsync(user, userF_Reg.password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userF_Reg.roles);
            }
            return result;
        }

        public async Task<bool> ValidateUser(UserLoginAuth userLoginAuth)
        {
            _user = await _userManager.FindByNameAsync(userLoginAuth.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userLoginAuth.Password));

            if (!result)
            {
                _logger.LogWarning($"{nameof(ValidateUser)} : Authentication failde Wrong E-mail or Password");
            }
            return result;

        }

        public async Task<User> ValidateUserCredentials(UserLoginAuth user)
        {
            var validUser = await _userManager.FindByNameAsync(user.UserName);
            if (validUser == null) return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(validUser, user.Password);
            if (isPasswordValid)
            {
                return validUser;
            }

            return null;
        }

        private SigningCredentials GetSigninCredentials()
        {
            var jwtSettings = _con.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;

        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _con.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signingCredentials
                );
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFropExpiredToken(string token)
        {
            var jwtSettings = _con.GetSection("JwrSettings");
            var secretKey = jwtSettings["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase)) 
            {

                throw new SecurityTokenException("Invalid token.");
            
            }
            return principal;
        }

        public async Task<tokenDto> RefreshToken(tokenDto tokenDto)
        {
            if (tokenDto == null || string.IsNullOrEmpty(tokenDto.AccessToken) || string.IsNullOrEmpty(tokenDto.RefreshToken))
            {
                throw new ArgumentNullException(nameof(tokenDto), "Token information is missing");
            }

            var principal = GetPrincipalFropExpiredToken(tokenDto.AccessToken);
            if (principal == null || principal.Identity == null || string.IsNullOrEmpty(principal.Identity.Name))
            {
                throw new Exception("Expired or invalid token");
            }

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (string.IsNullOrEmpty(user.RefreshToken) || user.RefreshToken != tokenDto.RefreshToken ||
                user.ResfreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Expired or invalid refresh token");
            }

            _user = user;
            return await CreateToken(populateExp: false);
        }

    }
}
