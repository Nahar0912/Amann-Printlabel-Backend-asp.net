using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly LabelService _service;

        public LabelController(LabelService service)
        {
            _service = service;
        }

        [HttpGet("index")]
        public IActionResult Index() => Ok("Label Service is running");

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var labels = await _service.GetAllAsync();
            return Ok(labels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var label = await _service.GetByIdAsync(id);
            if (label == null) return NotFound();
            return Ok(label);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(LabelDto dto)
        {
            var label = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = label.ID }, label);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateLabelDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
