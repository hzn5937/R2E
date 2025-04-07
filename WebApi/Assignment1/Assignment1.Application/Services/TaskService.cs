using Assignment1.Application.DTOs;
using Assignment1.Application.Extensions;
using Assignment1.Application.Interfaces;
using Assignment1.Domain.Entities;
using Assignment1.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TaskItemDto> CreateAsync(TaskCreateDto request)
        {
            var duplicate = await IsDuplicated(request.Title);

            if (duplicate == true)
            {
                throw new ConflictException($"A task with the title {request.Title.ToLower()} already exists.");
            }

            var task = _mapper.Map<TaskItem>(request);
            task.Id = Guid.NewGuid();

            var created = await _repository.CreateAsync(task);
            return _mapper.Map<TaskItemDto>(created);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        }

        public async Task<TaskItemDto?> GetByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task == null ? null : _mapper.Map<TaskItemDto>(task);
        }

        public async Task<TaskItemDto?> UpdateAsync(Guid id, TaskUpdateDto request)
        {
            bool duplicate = await IsDuplicated(request.Title);

            if (duplicate == true)
            {
                throw new ConflictException($"A task with the title {request.Title.ToLower()} already exists.");
            }

            var existing = await _repository.GetByIdAsync(id);

            if (existing == null) return null;

            existing.Title = request.Title;
            existing.IsCompleted = request.IsCompleted;

            var updated = await _repository.UpdateAsync(existing);
            return updated == null ? null : _mapper.Map<TaskItemDto>(updated);
        }

        public async Task<IEnumerable<TaskItemDto>> BulkCreateAsync(IEnumerable<TaskCreateDto> requests)
        {
            var distinctTasks = await RemoveDuplicates(requests);

            if (distinctTasks == null || !distinctTasks.Any())
            {
                throw new ConflictException("No unique tasks to create.");
            }

            var tasks = distinctTasks.Select(task => new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = task.Title,
                IsCompleted = task.IsCompleted
            });

            var created = await _repository.BulkCreateAsync(tasks);
            return _mapper.Map<IEnumerable<TaskItemDto>>(created);
        }

        public async Task<bool> BulkDeleteAsync(IEnumerable<Guid> ids)
        {
            return await _repository.BulkDeleteAsync(ids);
        }

        // Helper function to check for duplicates
        public async Task<bool> IsDuplicated(string title)
        {
            var existingTasks = await _repository.GetAllAsync();

            var result = existingTasks.Any(t => t.Title.Equals(title.Trim(), StringComparison.OrdinalIgnoreCase));

            return result;
        }

        public async Task<IEnumerable<TaskCreateDto>> RemoveDuplicates(IEnumerable<TaskCreateDto> requests)
        {
            var distinctTitleRequests = requests
                .GroupBy(x => x.Title.ToLower().Trim())
                .Select(g => g.First())
                .ToList();

            var existingTasks = await _repository.GetAllAsync();

            distinctTitleRequests.RemoveAll(requestTask => 
                existingTasks.Any(task => string.Equals(requestTask.Title.Trim(), task.Title, StringComparison.OrdinalIgnoreCase)));

            return distinctTitleRequests;
        }
    }
}
