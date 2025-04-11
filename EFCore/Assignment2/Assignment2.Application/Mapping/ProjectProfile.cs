using Assignment2.Application.DTOs.Projects;
using Assignment2.Domain.Entities;
using AutoMapper;

namespace Assignment2.Application.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Projects, ProjectOutputDto>();

            CreateMap<CreateProjectDto, Projects>();
            CreateMap<UpdateProjectDto, Projects>();
        }
    }
}
