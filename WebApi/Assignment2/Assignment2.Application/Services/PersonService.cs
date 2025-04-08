using Assignment2.Application.DTOs.Person;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Enums;
using Assignment2.Domain.Interfaces;
using AutoMapper;

namespace Assignment2.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonOutputDto> CreatePersonAsync(PersonCreateDto request)
        {
            Person person = _mapper.Map<Person>(request);

            Person created = await _personRepository.CreatePersonAsync(person);

            PersonOutputDto personOutputDto = _mapper.Map<PersonOutputDto>(created);

            return personOutputDto;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            bool isDeleted = await _personRepository.DeletePersonAsync(id);

            return isDeleted;
        }

        public async Task<PersonOutputDto?> UpdatePersonAsync(int id, PersonUpdateDto request)
        {
            Person? existing = await _personRepository.GetPersonByIdAsync(id);

            if (existing == null)
            {
                return null;
            }

            Person person = _mapper.Map<Person>(request);
            person.Id = id;

            Person updated = await _personRepository.UpdatePersonAsync(person);

            PersonOutputDto personOutputDto = _mapper.Map<PersonOutputDto>(updated);

            return personOutputDto;
        }

        public async Task<IEnumerable<PersonOutputDto>> FilterPeopleAsync(string? name, Gender? gender, string? birthPlace)
        {
            IEnumerable<Person> people = new List<Person>();

            people = await _personRepository.FilterPeopleAsync(name, gender, birthPlace);

            IEnumerable<PersonOutputDto> personOutputDtos = _mapper.Map<IEnumerable<PersonOutputDto>>(people);

            return personOutputDtos;
        }

        public async Task<PersonOutputDto?> GetPersonByIdAsync(int id)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);

            return person == null ? null : _mapper.Map<PersonOutputDto>(person);
        }
    }
}
