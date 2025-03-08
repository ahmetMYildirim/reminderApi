using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Services.Contract;
using System;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IUserService _userService;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("user/{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var reminder = _manager.userService.GetUserById(id);

            if (reminder == null)
                return NotFound();

            return Ok(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UsersDto usersDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Users
            {
                username = usersDto.username,
                email = usersDto.email,
                password = usersDto.password,
                created_at = DateTime.UtcNow
            };

            _manager.userService.CreateUser(user);
            return CreatedAtRoute("GetUserById", new { id = user.id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _manager.userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.username = userUpdateDto.username;
            existingUser.email = userUpdateDto.email;
            existingUser.password = userUpdateDto.password;

            _manager.userService.UpdateUser(id, existingUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
