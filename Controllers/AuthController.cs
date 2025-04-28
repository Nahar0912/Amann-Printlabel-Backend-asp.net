using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Backend.DTOs;

[Route("auth")]
[ApiController]

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
        var user = await _userService.FindByEmailAsync(userDto.Email);
        if (user == null || !_userService.ValidatePassword(userDto.PasswordHash, user.PasswordHash))
        {
            return BadRequest("Invalid credentials");
        }

        var token = _authService.GenerateJwtToken(user);

        Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
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
        return Ok(new { message = "User is authenticated" });
    }
}
