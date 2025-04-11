using Assignment2.Domain.Entities;

namespace Assignment2.Domain.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Departments>
    {
        Task<Departments?> GetByNameAsync(string name);
    }
}
