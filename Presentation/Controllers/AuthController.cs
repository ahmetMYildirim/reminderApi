using Entities.Auth_Models;
using Entities.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AuthController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost("register")]
        public async Task<IActionResult> regUser([FromBody] UserF_Reg userF_Reg)
        {
            var result = await _services.authenticationService.regUser(userF_Reg);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authencate([FromBody] UserLoginAuth user)
        {
            if (!await _services.authenticationService.ValidateUser(user))
            {
                return Unauthorized(); //401
            }
            var tokenDto = await _services
                .authenticationService
                .CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] tokenDto tokenDto)
        {
            var tokenDtoToReturn = await _services
                .authenticationService
                .RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateUser([FromBody] UserLoginAuth user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Kullanıcı adı ve şifre boş olamaz.");
            }

            var validUser = await _services.authenticationService.ValidateUserCredentials(user);

            if (validUser == null)
            {
                return Unauthorized();
            }

            return Ok(new { userId = validUser.Id, username = validUser.UserName });
        }
    }
}
