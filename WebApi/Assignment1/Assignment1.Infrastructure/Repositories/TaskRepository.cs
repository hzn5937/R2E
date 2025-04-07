using Assignment1.Domain.Entities;
using Assignment1.Domain.Interfaces;
using Assignment1.Infrastructure.Seed;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ConcurrentDictionary<Guid, TaskItem> _tasks = new();

        public TaskRepository()
        {
            // Seed with initial data (in-memory)
            var initialTasks = TaskDataSeeder.GetInitialTasks();

            // Set task.Id as the key to the actual TaskItem
            foreach (var task in initialTasks)
                _tasks[task.Id] = task;
        }

        public Task<IEnumerable<TaskItem>> GetAllAsync() =>
            Task.FromResult<IEnumerable<TaskItem>>(_tasks.Values.ToList());

        public Task<TaskItem?> GetByIdAsync(Guid id) =>
            Task.FromResult(_tasks.TryGetValue(id, out var task) ? task : null);

        public Task<TaskItem> CreateAsync(TaskItem task)
        {
            _tasks[task.Id] = task;
            return Task.FromResult(task);
        }

        public Task<bool> DeleteAsync(Guid id) =>
            Task.FromResult(_tasks.TryRemove(id, out _));

        public Task<TaskItem?> UpdateAsync(TaskItem task)
        {
            if (!_tasks.ContainsKey(task.Id))
                return Task.FromResult<TaskItem?>(null);

            _tasks[task.Id] = task;
            return Task.FromResult<TaskItem?>(task);
        }

        public Task<IEnumerable<TaskItem>> BulkCreateAsync(IEnumerable<TaskItem> tasks)
        {
            foreach (var task in tasks)
            {
                _tasks[task.Id] = task;
            }

            return Task.FromResult(tasks);
        }

        public Task<bool> BulkDeleteAsync(IEnumerable<Guid> ids)
        {
            bool success = true;

            foreach (var id in ids)
            {
                success &= _tasks.TryRemove(id, out _);
            }

            return Task.FromResult(success);
        }
    }
}
