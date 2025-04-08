using Assignment2.Application.DTOs.Person;
using Assignment2.Domain.Enums;

namespace Assignment2.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonOutputDto> CreatePersonAsync(PersonCreateDto person);
        Task<PersonOutputDto?> UpdatePersonAsync(int id, PersonUpdateDto person);
        Task<bool> DeletePersonAsync(int id);
        Task<IEnumerable<PersonOutputDto>> FilterPeopleAsync(string? name, Gender? gender, string? birthPlace);
        Task<PersonOutputDto?> GetPersonByIdAsync(int id);
    }
}
