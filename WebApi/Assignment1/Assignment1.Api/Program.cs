using Assignment1.Api.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Assignment1.Application.DTOs;
using Assignment1.Api.Middlewares;
using System;
using Assignment1.Api.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Pack all the services into a single method for better organization
builder.Services.AddApplicationServices();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new BooleanJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new GuidJsonConverter());
});

//builder.Services.AddValidatorsFromAssemblyContaining<TaskCreateValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<TaskUpdateValidator>();
//builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
