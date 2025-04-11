using Assignment2.Application.DTOs.Departments;
using Assignment2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartmentOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentService.GetAllAsync();

            if (departments == null || !departments.Any())
            {
                return NotFound("No departments found.");
            }

            return Ok(departments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DepartmentOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DepartmentOutputDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Department data is required.");
            }

            var createdDepartment = await _departmentService.CreateAsync(departmentDto);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(DepartmentOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Department data is required.");
            }

            var updatedDepartment = await _departmentService.UpdateAsync(id, departmentDto);

            if (updatedDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return Ok(updatedDepartment);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleted = await _departmentService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
