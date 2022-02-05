using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Services.UserService;
using backend.Models;

namespace backend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _config = config;
            _userService = userService;
        }

        // Edit User
        [HttpPut("{id}")]
        public async Task<object> UpdateUser(UserForEdit userForEdit)
        {
            var update = await _userService.EditUser(userForEdit);

            return Ok(new { updatedUser = update });
        }
    }
}