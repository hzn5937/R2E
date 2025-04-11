using Assignment2.Application.DTOs.Projects;
using Assignment2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();

            if (projects == null || !projects.Any())
            {
                return NotFound("No projects found.");
            }

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjectOutputDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is required.");
            }

            var createdProject = await _projectService.CreateAsync(projectDto);

            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(ProjectOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is required.");
            }

            var updatedProject = await _projectService.UpdateAsync(id, projectDto);

            if (updatedProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
