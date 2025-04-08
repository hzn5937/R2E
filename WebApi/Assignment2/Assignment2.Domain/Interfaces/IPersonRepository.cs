using Assignment2.Domain.Entities;
using Assignment2.Domain.Enums;

namespace Assignment2.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> CreatePersonAsync(Person person);
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(int id);
        Task<IEnumerable<Person>> FilterPeopleAsync(string? name, Gender? gender, string? birthPlace);
        Task<Person?> GetPersonByIdAsync(int id);
    }
}
