using Assignment2.Domain.Entities;
using Assignment2.Domain.Models;

namespace Assignment2.Domain.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employees>
    {
        public Task<IEnumerable<EmployeeWithDepartment>> GetAllWithDepartmentAsync();
        Task<IEnumerable<EmployeeWithProject>> GetAllWithProjectsAsync();
        Task<IEnumerable<HighSalaryRecentEmployee>> GetHighSalaryRecentEmployeesAsync();
    }
}
