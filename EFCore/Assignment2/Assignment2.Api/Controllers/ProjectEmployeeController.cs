using Assignment2.Application.DTOs.ProjectEmployees;
using Assignment2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [Route("api/projects/{projectId}/employees")]
    [ApiController]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly IProjectEmployeeService _projectEmployeeService;

        public ProjectEmployeeController(IProjectEmployeeService service)
        {
            _projectEmployeeService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectEmployeeOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(int projectId)
        {
            var result = await _projectEmployeeService.GetAllAsync(projectId);
            return Ok(result);
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(typeof(ProjectEmployeeOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int projectId, int employeeId)
        {
            var result = await _projectEmployeeService.GetAsync(projectId, employeeId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjectEmployeeOutputDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(int projectId, CreateProjectEmployeeDto dto)
        {
            var result = await _projectEmployeeService.CreateAsync(projectId, dto);
            return CreatedAtAction(nameof(Get), new { projectId, employeeId = result.EmployeeId }, result);
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(typeof(ProjectEmployeeOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int projectId, int employeeId, UpdateProjectEmployeeDto dto)
        {
            var updated = await _projectEmployeeService.UpdateAsync(projectId, employeeId, dto);
            
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int projectId, int employeeId)
        {
            var deleted = await _projectEmployeeService.DeleteAsync(projectId, employeeId);

            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
