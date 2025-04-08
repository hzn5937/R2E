using Assignment2.Domain.Entities;
using Assignment2.Domain.Enums;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Seed;
using System.Collections.Concurrent;

namespace Assignment2.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ConcurrentDictionary<int, Person> _people = new();
        private int _newId = 0;

        public PersonRepository()
        {
            IEnumerable<Person> initialPerson = PersonDataSeeder.GetInitialPeople();

            foreach (Person person in initialPerson)
            {
                _people[person.Id] = person;
            }
            _newId = _people.Keys.Max() + 1;
        }

        public Task<Person> CreatePersonAsync(Person person)
        {
            person.Id = _newId++;

            _people[person.Id] = person;
            return Task.FromResult(person);
        }

        public Task<Person?> GetPersonByIdAsync(int id)
        {
            var result = _people.TryGetValue(id, out Person? person) ? person : null;
            return Task.FromResult(result);
        }

        public Task<bool> DeletePersonAsync(int id)
        {
            return Task.FromResult(_people.TryRemove(id, out _));
        }

        public Task<Person> UpdatePersonAsync(Person person)
        {
            _people[person.Id] = person;
            return Task.FromResult(person);
        }

        public Task<IEnumerable<Person>> FilterPeopleAsync(string? name, Gender? gender, string? birthPlace)
        {
            var result = _people.Values.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(p => ($"{p.FirstName} {p.LastName}").Contains(name));
            }

            if (gender != null)
            {
                result = result.Where(p => p.Gender.Equals(gender));
            }

            if (!string.IsNullOrEmpty(birthPlace))
            {
                result = result.Where(p => p.BirthPlace.Contains(birthPlace));
            }

            return Task.FromResult(result.AsEnumerable());
        }
    }
}
