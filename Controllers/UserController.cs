using Backend.DTOs;
using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpGet]
        public Task<ActionResult<IEnumerable<UserEntity>>> GetAll()
        {
            var users = _userService.FindAll();
            return Task.FromResult<ActionResult<IEnumerable<UserEntity>>>(Ok(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> GetById(int id)
        {
            var user = await _userService.FindOneByIdAsync(id);
            return Ok(user);
        }

        [HttpPost("add")]
        public async Task<ActionResult<UserEntity>> Create([FromBody] UserDto userDto)
        {
            var user = await _userService.CreateAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<ActionResult<UserEntity>> Update(int id, [FromBody] UpdateUserDto userDto)
        {
            var user = await _userService.UpdateAsync(id, userDto);
            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok(new { message = $"User with ID {id} successfully deleted" });
        }
    }
}
