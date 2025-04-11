using Assignment2.Api.Helpers;
using Assignment2.Api.Swagger;
using Assignment2.Application.DTOs.Departments;
using Assignment2.Application.Interfaces;
using Assignment2.Application.Mapping;
using Assignment2.Application.Services;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Assignment2.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DepartmentProfile));
            services.AddAutoMapper(typeof(EmployeeProfile));

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<ISalaryRepository, SalaryRepository>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<IProjectEmployeeService, ProjectEmployeeService>();
            services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();

            services.AddValidatorsFromAssemblyContaining<CreateDepartmentDto>();
            services.AddFluentValidationAutoValidation();

            services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();
                c.SchemaFilter<DateOnlySchemaFilter>();
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new StringJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new IntJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new BooleanJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DecimalJsonConverter());
            });

            return services;
        }
    }
}
