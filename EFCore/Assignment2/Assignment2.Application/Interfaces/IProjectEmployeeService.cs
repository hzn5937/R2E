using Assignment2.Application.DTOs.ProjectEmployees;

namespace Assignment2.Application.Interfaces
{
    public interface IProjectEmployeeService
    {
        Task<IEnumerable<ProjectEmployeeOutputDto>> GetAllAsync(int projectId);
        Task<ProjectEmployeeOutputDto?> GetAsync(int projectId, int employeeId);
        Task<ProjectEmployeeOutputDto> CreateAsync(int projectId, CreateProjectEmployeeDto dto);
        Task<ProjectEmployeeOutputDto?> UpdateAsync(int projectId, int employeeId, UpdateProjectEmployeeDto dto);
        Task<bool> DeleteAsync(int projectId, int employeeId);
    }
}
