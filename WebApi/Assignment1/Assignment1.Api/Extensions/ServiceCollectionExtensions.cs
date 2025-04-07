using Assignment1.Application.Interfaces;
using Assignment1.Application.Mapping;
using Assignment1.Application.Services;
using Assignment1.Domain.Interfaces;
using Assignment1.Infrastructure.Repositories;

namespace Assignment1.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TaskProfile));
            services.AddScoped<ITaskService, TaskService>();
            services.AddSingleton<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
