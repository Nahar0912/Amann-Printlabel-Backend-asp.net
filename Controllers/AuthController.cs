using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = await _userService.FindByEmail(userDto.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid User credentials" });
            }

            var isPasswordValid = await _userService.ValidatePassword(userDto.PasswordHash, user.PasswordHash);
            if (!isPasswordValid)
            {
                return BadRequest(new { message = "Invalid Password credentials" });
            }

            var token = _authService.GenerateToken(user);

            // Set the JWT in an HTTP-only cookie
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                // MaxAge = TimeSpan.FromHours(1)
            });

            return Ok(new { message = "Login successful" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("check")]
        [Authorize]
        public IActionResult CheckAuth()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User is not authenticated" });
            }

            return Ok(new { message = "User is authenticated", userId, email });
        }
    }
}
