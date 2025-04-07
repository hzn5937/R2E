using Assignment1.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllAsync();
        Task<TaskItemDto?> GetByIdAsync(Guid id);
        Task<TaskItemDto> CreateAsync(TaskCreateDto task);
        Task<bool> DeleteAsync(Guid id);
        Task<TaskItemDto?> UpdateAsync(Guid id, TaskUpdateDto task);

        Task<IEnumerable<TaskItemDto>> BulkCreateAsync(IEnumerable<TaskCreateDto> tasks);
        Task<bool> BulkDeleteAsync(IEnumerable<Guid> ids);
    }
}
