using Assignment2.Api.Helpers;
using Assignment2.Api.Swagger;
using Assignment2.Application.Interfaces;
using Assignment2.Application.Mapping;
using Assignment2.Application.Services;
using Assignment2.Application.Validators.Person;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace Assignment2.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PersonProfile));
            services.AddScoped<IPersonService, PersonService>();
            services.AddSingleton<IPersonRepository, PersonRepository>();

            
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new StringJsonConverter());
                // Needed to place before JsonStringEnumConverter (because ASP.NET core will use that by default when added)
                options.JsonSerializerOptions.Converters.Add(new GenderJsonConverter());
                // Show Enum as String Value in JSON (swagger)
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

            // Add FluentValidation
            services.AddValidatorsFromAssemblyContaining<PersonCreateDtoValidator>();
            services.AddFluentValidationAutoValidation();

            // Change the default example to the correct format
            services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();
                c.SchemaFilter<DateOnlySchemaFilter>();
            });

            return services;
        }   
    }
}
