using Assignment1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> DeleteAsync(Guid id);
        Task<TaskItem?> UpdateAsync(TaskItem task);

        Task<IEnumerable<TaskItem>> BulkCreateAsync(IEnumerable<TaskItem> tasks);
        Task<bool> BulkDeleteAsync(IEnumerable<Guid> ids);
    }
}
