using Assignment2.Application.DTOs.Departments;
using Assignment2.Domain.Entities;
using AutoMapper;

namespace Assignment2.Application.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Departments, DepartmentOutputDto>();

            CreateMap<CreateDepartmentDto, Departments>();
            CreateMap<UpdateDepartmentDto, Departments>();
        }
    }
}
