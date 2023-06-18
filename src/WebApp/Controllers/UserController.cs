using Application.Contracts.User;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest userRegisterRequest)
        {
            var response = await _userService.Register(
                userRegisterRequest.UserName,
                userRegisterRequest.Password);

            if (!response.Success)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse<string>>> Login([FromBody] UserRequest userRequest)
        {
            var response = await _userService.Login(
                userRequest.UserName,
                userRequest.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
