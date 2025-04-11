using Assignment2.Application.DTOs.ProjectEmployees;
using Assignment2.Domain.Entities;
using AutoMapper;

namespace Assignment2.Application.Mapping
{
    public class ProjectEmployeeProfile : Profile
    {
        public ProjectEmployeeProfile()
        {
            CreateMap<ProjectEmployees, ProjectEmployeeOutputDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
            CreateMap<CreateProjectEmployeeDto, ProjectEmployees>();
            CreateMap<UpdateProjectEmployeeDto, ProjectEmployees>();
        }
    }
}
