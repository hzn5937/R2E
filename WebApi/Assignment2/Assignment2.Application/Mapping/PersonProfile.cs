using Assignment2.Application.DTOs.Person;
using Assignment2.Domain.Entities;
using AutoMapper;

namespace Assignment2.Application.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();

            CreateMap<Person, PersonOutputDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
