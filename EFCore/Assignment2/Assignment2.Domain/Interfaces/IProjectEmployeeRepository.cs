using Assignment2.Domain.Entities;

namespace Assignment2.Domain.Interfaces
{
    public interface IProjectEmployeeRepository
    {
        Task<IEnumerable<ProjectEmployees>> GetAllAsync(int projectId);
        Task<ProjectEmployees?> GetAsync(int projectId, int employeeId);
        Task<ProjectEmployees> CreateAsync(ProjectEmployees entity);
        Task<bool> UpdateAsync(ProjectEmployees entity);
        Task<bool> DeleteAsync(ProjectEmployees entity);
        Task<bool> ExistsAsync(int projectId, int employeeId);
    }
}
