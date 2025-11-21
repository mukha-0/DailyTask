using DailyTask.Domain.Enums;
using DailyTask.Service;
using DailyTask.Service.Services.DailyTask;
using DailyTask.Service.Services.DailyTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyTaskApiController : ControllerBase
    {
        private readonly IDailyTask _service;

        public DailyTaskApiController(IDailyTask service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DailyTaskCreateModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _service.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DailyTaskUpdateModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(id, task);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromQuery] TaskStatuss status)
        {
            await _service.ChangeStatusAsync(id, status);
            return Ok(new { Message = "Status updated successfully." });
        }
    }
}
