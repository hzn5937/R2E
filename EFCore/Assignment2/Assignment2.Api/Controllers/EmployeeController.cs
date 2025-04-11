using Assignment2.Application.DTOs.Employees;
using Assignment2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound("No employee found.");
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeOutputDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is required.");
            }

            var createdEmployee = await _employeeService.CreateAsync(employeeDto);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(EmployeeOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if (updateEmployeeDto == null)
            {
                return BadRequest("Department data is required.");
            }

            var updatedEmployee = await _employeeService.UpdateAsync(id, updateEmployeeDto);

            if (updatedEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return NoContent();
        }

        // item 3
        [HttpGet("with-department")]
        public async Task<IActionResult> GetEmployeesWithDepartment()
        {
            var employees = await _employeeService.GetAllWithDepartmentsAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound("No employee found.");
            }

            return Ok(employees);
        }

        [HttpGet("with-projects")]
        public async Task<IActionResult> GetEmployeesWithProjects()
        {
            var employees = await _employeeService.GetAllWithProjectsAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound("No employee found.");
            }

            return Ok(employees);
        }

        [HttpGet("high-salary-recently-joined")]
        public async Task<IActionResult> GetHighSalaryRecentlyJoined()
        {
            var employees = await _employeeService.GetHighSalaryRecentEmployeesAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound("No employee found.");
            }

            return Ok(employees);
        }
    }
}
