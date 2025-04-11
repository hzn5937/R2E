using Assignment2.Application.DTOs.Salaries;
using Assignment2.Domain.Entities;
using AutoMapper;

namespace Assignment2.Application.Mapping
{
    public class SalaryProfile : Profile
    {
        public SalaryProfile()
        {
            CreateMap<Salaries, SalaryOutputDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));

            CreateMap<CreateSalaryDto, Salaries>();
            CreateMap<UpdateSalaryDto, Salaries>();
        }
    }
}
