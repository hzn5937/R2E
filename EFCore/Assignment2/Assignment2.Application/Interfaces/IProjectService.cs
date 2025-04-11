using Assignment2.Application.DTOs.Projects;

namespace Assignment2.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectOutputDto>> GetAllAsync();
        Task<ProjectOutputDto?> GetByIdAsync(int id);
        Task<ProjectOutputDto?> CreateAsync(CreateProjectDto projectDto);
        Task<ProjectOutputDto?> UpdateAsync(int id, UpdateProjectDto projectDto);
        Task<bool> DeleteAsync(int id);

    }
}
