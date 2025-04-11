using Assignment2.Application.DTOs.Projects;
using Assignment2.Application.Extensions;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using AutoMapper;

namespace Assignment2.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectOutputDto?> CreateAsync(CreateProjectDto createProjectDto)
        {
            if (await ProjectExistsAsync(createProjectDto.Name))
            {
                throw new ConflictException($"A project with the name of '{createProjectDto.Name}' already exists");
            }

            var project = _mapper.Map<Projects>(createProjectDto);
            var createdProject = await _projectRepository.CreateAsync(project);
            var output = _mapper.Map<ProjectOutputDto>(createdProject);
            return output;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return false;
            }

            await _projectRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ProjectOutputDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectOutputDto>>(projects);
        }

        public async Task<ProjectOutputDto?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return null;
            }

            var output = _mapper.Map<ProjectOutputDto>(project);
            return output;
        }

        public async Task<ProjectOutputDto?> UpdateAsync(int id, UpdateProjectDto updateProjectDto)
        {
            if (await ProjectExistsAsync(updateProjectDto.Name) && updateProjectDto.Name != null)
            {
                throw new ConflictException($"A project with the name of '{updateProjectDto.Name}' already exists");
            }

            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return null;
            }

            _mapper.Map(updateProjectDto, project);
            var updatedProject = _projectRepository.UpdateAsync(project);
            var output = _mapper.Map<ProjectOutputDto>(updatedProject);
            return output;
        }

        public async Task<bool> ProjectExistsAsync(string projectName)
        {
            var project = await _projectRepository.GetByNameAsync(projectName);

            if (project == null)
            {
                return false;
            }

            return true;
        }
    }
}
