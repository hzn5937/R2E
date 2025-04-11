using Assignment2.Application.DTOs.Salaries;
using Assignment2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [Route("api/salaries")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService, ILogger<SalaryController> logger)
        {
            _salaryService = salaryService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SalaryOutputDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSalary([FromBody] CreateSalaryDto salaryDto)
        {
            if (salaryDto == null)
            {
                return BadRequest("Salary data is required.");

            }
            var createdSalary = await _salaryService.CreateAsync(salaryDto);

            return CreatedAtAction(nameof(GetSalaryById), new { id = createdSalary.Id }, createdSalary);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalaryOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalaries()
        {
            var salaries = await _salaryService.GetAllAsync();
            if (salaries == null || !salaries.Any())
            {
                return NotFound("No salary found.");
            }
            return Ok(salaries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalaryOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalaryById(int id)
        {
            var salary = await _salaryService.GetByIdAsync(id);
            if (salary == null)
            {
                return NotFound($"Salary with ID {id} not found.");
            }
            return Ok(salary);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(SalaryOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSalary([FromBody] UpdateSalaryDto salaryDto)
        {
            if (salaryDto == null)
            {
                return BadRequest("Salary data is required.");
            }

            var updatedSalary = await _salaryService.UpdateAsync(salaryDto);

            return Ok(updatedSalary);
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSalary(int employeeId)
        {
            var deleted = await _salaryService.DeleteAsync(employeeId);

            if (!deleted)
            {
                return NotFound($"Employee with ID {employeeId} not found.");
            }

            return NoContent();
        }
    }
}
