# Assignment 2 - Person Management API

## Overview

This project is a .NET 8 Web API application designed to manage information about people. It provides basic CRUD (Create, Read, Update, Delete) operations for person records, along with filtering capabilities. The application follows a standard layered architecture (API, Application, Domain, Infrastructure) and utilizes an in-memory data store for simplicity.

## Core Functionality

The core function of this API is to manage a list of `Person` entities. Key features include:

* **Create:** Add a new person record.
* **Read:** Retrieve a specific person by their ID or filter the list based on name, gender, or birthplace.
* **Update:** Modify an existing person's details.
* **Delete:** Remove a person record from the system.

The data is currently stored in memory using a `ConcurrentDictionary` within the `PersonRepository`, which is seeded with initial data upon application startup.

## Project Structure and Key Components

### 1. Assignment2.Domain

* **Entities:** Contains the core `Person` entity definition and the `Gender` enum.
* **Interfaces:** Defines the contract for the data repository (`IPersonRepository`).

### 2. Assignment2.Application

* **DTOs (`Assignment2.Application.DTOs`):** Data Transfer Objects used for API requests and responses (`PersonCreateDto`, `PersonUpdateDto`, `PersonOutputDto`). These help decouple the API layer from the domain model.
* **Interfaces:** Defines the contract for the application service (`IPersonService`).
* **Services:** Contains the business logic implementation (`PersonService`) which orchestrates operations using the repository and AutoMapper.
* **Mapping (`Assignment2.Application.Mapping`):** Contains AutoMapper profiles (`PersonProfile`) to handle the mapping between Domain Entities (`Person`) and DTOs. It includes logic to combine `FirstName` and `LastName` into `FullName` for the output DTO.
* **Validators (`Assignment2.Validators`):** Uses FluentValidation to define and enforce validation rules for the input DTOs (`PersonCreateDtoValidator`, `PersonUpdateDtoValidator`). Rules include checking for required fields, maximum lengths, and valid date ranges.

### 3. Assignment2.Infrastructure

* **Repositories:** Implements the data access logic (`PersonRepository`) using an in-memory collection (`ConcurrentDictionary`).
* **Seed:** Provides initial sample data (`PersonDataSeeder`) for the in-memory repository.

### 4. Assignment2.Api

* **Controllers:** Handles incoming HTTP requests (`PersonController`), validates input, calls the application service, and formats the responses. Includes endpoints for all CRUD operations and filtering. Logging is implemented for error handling.
* **Extensions (`Assignment2.Api.Extensions`):** The `ServiceCollectionExtensions` class centralizes the registration of application services, repositories, AutoMapper, FluentValidation, and Swagger configuration using dependency injection. It also configures JSON serialization options, including custom converters.
* **Helpers (`Assignment2.Api.Helpers`):** Contains custom JSON converters (`DateOnlyJsonConverter`, `GenderJsonConverter`, `StringJsonConverter`) to ensure correct serialization and deserialization of specific data types like `DateOnly` and the `Gender` enum.
* **Middlewares (`Assignment2.Api.Middlewares`):** Includes `ExceptionMiddleware` for global exception handling. It's designed to catch unhandled exceptions and return consistent JSON error responses. *(Note: The implementation in `Invoke` currently bypasses the `HandleExceptionAsync` logic, which might be unintentional)*.
* **Swagger (`Assignment2.Swagger`):** Swagger is configured via `ServiceCollectionExtensions` to provide API documentation. A custom `DateOnlySchemaFilter` is used to ensure `DateOnly` types are displayed correctly in the Swagger UI documentation. The Swagger UI is enabled in the development environment.
* **Program.cs:** The main entry point for the API. It sets up the web application builder, registers services using the `AddApplicationServices` extension method, configures the HTTP request pipeline (including HTTPS redirection, authorization, Swagger), and maps the controllers.

## How to Run

1.  **Prerequisites:**
    * .NET 8 SDK installed.
    * An IDE like Visual Studio or VS Code (optional, can use dotnet CLI).
2.  **Clone/Download:** Get the project code.
3.  **Navigate:** Open a terminal or command prompt and navigate to the `Assignment2.Api` directory.
4.  **Run:** Execute the following command:
    ```bash
    dotnet run
    ```
5.  **Access API:**
    * The API will typically start on `https://localhost:7164` and `http://localhost:5092` (check the console output for exact URLs).
    * Access the Swagger UI for interactive API documentation by navigating to `/swagger` on the running instance (e.g., `https://localhost:7164/swagger`).
