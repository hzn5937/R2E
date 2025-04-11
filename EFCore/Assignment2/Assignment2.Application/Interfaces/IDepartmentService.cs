using Assignment2.Application.DTOs.Departments;

namespace Assignment2.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentOutputDto>> GetAllAsync();
        Task<DepartmentOutputDto?> GetByIdAsync(int id);
        Task<DepartmentOutputDto?> CreateAsync(CreateDepartmentDto department);
        Task<DepartmentOutputDto?> UpdateAsync(int id, UpdateDepartmentDto department);
        Task<bool> DeleteAsync(int id);
    }
}
