using Assignment2.Domain.Entities;

namespace Assignment2.Domain.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Projects>
    {
        Task<Projects?> GetByNameAsync(string name);
    }
}
