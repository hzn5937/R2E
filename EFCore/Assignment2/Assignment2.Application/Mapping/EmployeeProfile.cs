using Assignment2.Application.DTOs.Employees;
using Assignment2.Domain.Entities;
using AutoMapper;

namespace Assignment2.Application.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employees, EmployeeOutputDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreateEmployeeDto, Employees>();
            CreateMap<UpdateEmployeeDto, Employees>();
        }
    }
}
