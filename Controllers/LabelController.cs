using Backend.DTOs;
using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("labels")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly LabelService _labelService;

        public LabelController(LabelService labelService)
        {
            _labelService = labelService;
        }

        // Create a new label
        [HttpPost("add")]
        public async Task<ActionResult<LabelEntity>> Create([FromBody] LabelDto labelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var label = await _labelService.CreateLabelAsync(labelDto);
            return Ok(label);
        }

        // Get all labels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabelEntity>>> GetAll()
        {
            var labels = await _labelService.GetAllLabelsAsync();
            return Ok(labels);
        }

        // Get label by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<LabelEntity>> GetById(int id)
        {
            var label = await _labelService.GetLabelByIdAsync(id);
            if (label == null)
                return NotFound();

            return Ok(label);
        }

        // Update a label
        //[Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<LabelEntity>> Update(int id, [FromBody] UpdateLabelDto dto)
        {
            var updated = await _labelService.UpdateLabelAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // Delete a label
        //[Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _labelService.DeleteLabelAsync(id);
            if (!success)
                return NotFound();

            return Ok(new { message = "Deleted successfully" });
        }
    }
}
