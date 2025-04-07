using Assignment1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Infrastructure.Seed
{
    public static class TaskDataSeeder
    {
        public static IEnumerable<TaskItem> GetInitialTasks()
        {
            return new List<TaskItem>
            {
                new TaskItem { Id = Guid.NewGuid(), Title = "Do assignment", IsCompleted = false },
                new TaskItem { Id = Guid.NewGuid(), Title = "Clean room", IsCompleted = true },
                new TaskItem { Id = Guid.NewGuid(), Title = "Study new concept", IsCompleted = false }
            };
        }
    }
}
