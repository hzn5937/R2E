using Assignment1.Application.DTOs;
using Assignment1.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskService.GetAllAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting tasks");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);

                if (task == null)
                    return NotFound();

                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting task with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDto task)
        {
            try
            {
                var createdTask = await _taskService.CreateAsync(task);

                return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating task");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateDto task)
        {
            try
            {
                var updatedTask = await _taskService.UpdateAsync(id, task);

                if (updatedTask == null)
                    return NotFound();

                return Ok(updatedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating task with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _taskService.DeleteAsync(id);

                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting task with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("/api/Tasks")]
        public async Task<IActionResult> BulkCreate([FromBody] IEnumerable<TaskCreateDto> tasks)
        {
            try
            {
                if (tasks == null || !tasks.Any())
                    return BadRequest();

                var createdTasks = await _taskService.BulkCreateAsync(tasks);

                return CreatedAtAction(nameof(GetAll), createdTasks);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while creating tasks");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("/api/Tasks")]
        public async Task<IActionResult> BulkDelete([FromBody] IEnumerable<Guid> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    return BadRequest();

                var deleted = await _taskService.BulkDeleteAsync(ids);

                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while deleting tasks");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
