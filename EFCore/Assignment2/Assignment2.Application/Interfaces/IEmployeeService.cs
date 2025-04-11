using Assignment2.Application.DTOs.Employees;
using Assignment2.Domain.Models;

namespace Assignment2.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeOutputDto>> GetAllAsync();
        Task<EmployeeOutputDto?> GetByIdAsync(int id);
        Task<EmployeeOutputDto> CreateAsync(CreateEmployeeDto employeeDto);
        Task<EmployeeOutputDto?> UpdateAsync(int id, UpdateEmployeeDto employeeDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EmployeeWithDepartment>> GetAllWithDepartmentsAsync();
        Task<IEnumerable<EmployeeWithProject>> GetAllWithProjectsAsync();
        Task<IEnumerable<HighSalaryRecentEmployee>> GetHighSalaryRecentEmployeesAsync();
    }
}
