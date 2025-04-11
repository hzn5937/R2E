using Assignment2.Application.DTOs.ProjectEmployees;
using Assignment2.Application.Extensions;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using AutoMapper;

namespace Assignment2.Application.Services
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public ProjectEmployeeService(IProjectEmployeeRepository projectEmployeeRepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _projectEmployeeRepository = projectEmployeeRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectEmployeeOutputDto>> GetAllAsync(int projectId)
        {
            var entities = await _projectEmployeeRepository.GetAllAsync(projectId);
            return _mapper.Map<IEnumerable<ProjectEmployeeOutputDto>>(entities);
        }

        public async Task<ProjectEmployeeOutputDto?> GetAsync(int projectId, int employeeId)
        {
            var entity = await _projectEmployeeRepository.GetAsync(projectId, employeeId);
            return entity == null ? null : _mapper.Map<ProjectEmployeeOutputDto>(entity);
        }

        public async Task<ProjectEmployeeOutputDto> CreateAsync(int projectId, CreateProjectEmployeeDto dto)
        {
            var projectExists = await _projectRepository.GetByIdAsync(projectId);
            var employeeExists = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
            var alreadyLinked = await _projectEmployeeRepository.ExistsAsync(projectId, dto.EmployeeId);

            if (projectExists == null)
            {
                throw new NotFoundException($"Project ID {projectId} not found.");
            }

            if (employeeExists == null)
            {
                throw new NotFoundException($"Employee ID {dto.EmployeeId} not found.");
            }

            if (alreadyLinked)
            {
                throw new ConflictException("This employee is already assigned to the project.");
            }    

            var entity = new ProjectEmployees
            {
                ProjectId = projectId,
                EmployeeId = dto.EmployeeId,
                Enable = dto.Enable
            };

            var created = await _projectEmployeeRepository.CreateAsync(entity);
            var output = _mapper.Map<ProjectEmployeeOutputDto>(created);
            return output;
        }

        public async Task<ProjectEmployeeOutputDto?> UpdateAsync(int projectId, int employeeId, UpdateProjectEmployeeDto dto)
        {
            var existing = await _projectEmployeeRepository.GetAsync(projectId, employeeId);
            if (existing == null)
            {
                return null;
            }

            existing.Enable = dto.Enable;

            var updated = await _projectEmployeeRepository.UpdateAsync(existing);
            var output = _mapper.Map<ProjectEmployeeOutputDto>(updated);
            return output;
        }

        public async Task<bool> DeleteAsync(int projectId, int employeeId)
        {
            var existing = await _projectEmployeeRepository.GetAsync(projectId, employeeId);

            if (existing == null)
            {
                return false;
            }

            return await _projectEmployeeRepository.DeleteAsync(existing);
        }

    }
}
