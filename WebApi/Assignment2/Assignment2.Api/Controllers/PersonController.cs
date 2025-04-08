using Assignment2.Application.DTOs.Person;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }


        [HttpPost]
        [ProducesResponseType(typeof(PersonOutputDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePerson([FromBody] PersonCreateDto personCreateDto)
        {
            try
            {
                if (personCreateDto == null)
                {
                    return BadRequest("Person data is null");
                }

                var createdPerson = await _personService.CreatePersonAsync(personCreateDto);

                return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex} An error occurred while creating a person: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PersonOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonUpdateDto personUpdateDto)
        {
            try
            {
                if (personUpdateDto == null)
                {
                    return BadRequest("Person data is null");
                }

                var updatedPerson = await _personService.UpdatePersonAsync(id, personUpdateDto);

                if (updatedPerson == null)
                {
                    return NotFound();
                }

                return Ok(updatedPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex} An error occurred while updating a person: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var isDeleted = await _personService.DeletePersonAsync(id);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex} An error occurred while deleting a person: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet]
        [Route("/api/People")]
        [ProducesResponseType(typeof(IEnumerable<PersonOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FilterPeople([FromQuery] string? name, [FromQuery] Gender? gender, [FromQuery] string? birthPlace)
        {
            try
            {
                var people = await _personService.FilterPeopleAsync(name, gender, birthPlace);

                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex} An error occurred while filtering people: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);
            
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}
