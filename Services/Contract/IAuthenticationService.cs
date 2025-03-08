using Entities.Auth_Models;
using Entities.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> regUser(UserF_Reg userF_Reg);
        Task<IdentityUser> GetUserByEmail(string email);
        Task<bool> ValidateUser(UserLoginAuth userLoginAuth);
        Task<tokenDto> CreateToken(bool populateExp);
        Task<tokenDto> RefreshToken(tokenDto tokenDto);
        Task<User> ValidateUserCredentials(UserLoginAuth user);
    }
}
