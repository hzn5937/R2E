# Assignment 2 - Project README

## Project Overview

This project is a .NET 8 Web API application designed to manage employees, departments, projects, and salaries. It follows a standard layered architecture, separating concerns into Domain, Application, Infrastructure, and API layers. The project utilizes Entity Framework Core for data access with a SQL Server database and incorporates features like DTOs, validation, exception handling, and data seeding.

## Core Components & Functionality

### `Assignment2.Api`

This is the presentation layer, responsible for exposing the application's functionality via RESTful endpoints.

* **Controllers (`Assignment2.Api/Controllers`)**: Define the API endpoints for each entity (Departments, Employees, Projects, ProjectEmployees, Salaries). They handle incoming HTTP requests, interact with application services, and return appropriate HTTP responses.
* **Helpers (`Assignment2.Api/Helpers`)**: Contains custom JSON converters (`DateOnlyJsonConverter`, `BooleanJsonConverter`, `DecimalJsonConverter`, `IntJsonConverter`, `StringJsonConverter`). These ensure proper serialization and deserialization of specific data types in API requests/responses and contribute to type safety by throwing specific `JsonException` errors when input data doesn't match the expected format (e.g., an invalid date string for `DateOnly`). These exceptions are then handled by the global exception middleware.
* **Extensions (`Assignment2.Api/Extensions/ServiceCollectionExtensions.cs`)**: Provides an extension method (`AddApplicationServices`) to centralize the registration of application and infrastructure services, AutoMapper profiles, FluentValidation validators, Swagger configuration, and custom JSON converters within the dependency injection container.
* **Middlewares (`Assignment2.Api/Middlewares/ExceptionMiddleware.cs`)**: Implements custom middleware for global exception handling. It catches exceptions thrown during request processing, logs them, and returns standardized JSON error responses with appropriate HTTP status codes (e.g., NotFound, Conflict, BadRequest, InternalServerError) based on custom exception types.
* **Swagger (`Assignment2.Api/Swagger/DateOnlySchemaFilter.cs`)**: Includes a custom schema filter for Swagger/OpenAPI documentation. As Swagger UI might incorrectly display the format or example for `DateOnly` types, this filter ensures that `DateOnly` properties are correctly represented as 'string' with 'date' format (`YYYY-MM-DD`) and provides a clear example (`2000-01-01`) in the Swagger UI input fields.
* **`Program.cs` (`Assignment2.Api/Program.cs`)**: The main entry point of the API application. It sets up the web application builder, configures services (including DbContext with the connection string from `appsettings.json`, controllers, Swagger, and application-specific services via `AddApplicationServices`), and defines the HTTP request processing pipeline (including the custom exception middleware, HTTPS redirection, authorization, and controller mapping). The application logic is now contained within the `Main` function.
* **`appsettings.json`**: Contains configuration settings, notably the database connection string (`DefaultConnection`).

### `Assignment2.Application`

This layer contains the application logic, coordinating tasks and directing the domain layer.

* **DTOs (`Assignment2.Application/DTOs`)**: Data Transfer Objects are used to define the shape of data entering and leaving the application via the API. There are specific DTOs for creating, updating, and outputting data for each entity (Departments, Employees, Projects, ProjectEmployees, Salaries), preventing direct exposure of domain entities.
* **Validators (`Assignment2.Application/Validators`)**: Uses FluentValidation to define validation rules for the input DTOs (Create/Update). These validators ensure data integrity before processing begins (e.g., checking for required fields, maximum lengths, valid ranges). Validators are registered automatically via the `AddApplicationServices` extension.
* **Mapping (`Assignment2.Application/Mapping`)**: Contains AutoMapper profiles (`DepartmentProfile`, `EmployeeProfile`, `ProjectProfile`, `SalaryProfile`, `ProjectEmployeeProfile`) that define how to map between domain entities (`Assignment2.Domain.Entities`) and their corresponding DTOs (`Assignment2.Application.DTOs`).
* **Services (`Assignment2.Application/Services`)**: Contain the core business logic. Services like `DepartmentService`, `EmployeeService`, etc., orchestrate operations by interacting with repositories and performing necessary logic (e.g., checking for duplicates before creating).
* **Interfaces (`Assignment2.Application/Interfaces`)**: Defines contracts for the services (e.g., `IDepartmentService`, `IEmployeeService`), promoting loose coupling.
* **Extensions (`Assignment2.Application/Extensions`)**: Defines custom exception types (`NotFoundException`, `ConflictException`, `TransactionFailedException`) used by services to indicate specific error conditions, which are then handled by the `ExceptionMiddleware` in the API layer.

### `Assignment2.Domain`

This layer represents the core business entities and rules.

* **Entities (`Assignment2.Domain/Entities`)**: Contains the Plain Old CLR Objects (POCOs) that represent the core data structures (Departments, Employees, Projects, Salaries, ProjectEmployees) and their relationships.
* **Models (`Assignment2.Domain/Models`)**: Contains specific model classes (`EmployeeWithDepartment`, `EmployeeWithProject`, `HighSalaryRecentEmployee`) designed to hold results from custom or complex queries, particularly those involving raw SQL. These are used in the `EmployeeRepository` for specific reporting tasks.
* **Interfaces (`Assignment2.Domain/Interfaces`)**:
    * `IGenericRepository<T>`: Defines a generic repository pattern interface with standard CRUD operations (GetAllAsync, GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync).
    * Specific Repository Interfaces (e.g., `IDepartmentRepository`, `IEmployeeRepository`, `IProjectRepository`, `ISalaryRepository`, `IProjectEmployeeRepository`): Inherit from `IGenericRepository` (where applicable) and define additional methods specific to each entity (e.g., `GetByNameAsync`, `GetAllWithDepartmentAsync`).

### `Assignment2.Infrastructure`

This layer handles data access and other external concerns.

* **Data (`Assignment2.Infrastructure/Data`)**:
    * `AppDbContext`: The Entity Framework Core DbContext class. It defines the `DbSet` properties for entities and configures the database model using `EntityTypeConfiguration` classes and applies data seeding via `ModelBuilderSeeder`.
    * `ModelBuilderSeeder`: A static class responsible for seeding initial data for Departments, Employees, Salaries, Projects, and ProjectEmployees when the database model is created. This separates seeding logic from the DbContext configuration.
* **Repositories (`Assignment2.Infrastructure/Repositories`)**: Implement the domain repository interfaces (e.g., `DepartmentRepository`, `EmployeeRepository`). They contain the data access logic using Entity Framework Core (LINQ queries, `AddAsync`, `Update`, `Remove`, raw SQL via `SqlQueryRaw`) and manage database transactions.
* **EntityConfigurations (`Assignment2.Infrastructure/EntityConfigurations`)**: Classes that implement `IEntityTypeConfiguration<T>` to configure the mapping between domain entities and database tables, including primary keys, relationships, constraints (like required fields, max length), and table names (e.g., explicitly mapping `ProjectEmployees` to the "Project_Employee" table).
* **Migrations (`Assignment2.Infrastructure/Migrations`)**: Contain EF Core migration files that track changes to the database schema over time, including the initial creation and data seeding.

## Improvements from Last Assignment

* **`Program.cs` in `Main` Function**: The application's setup and configuration logic in the API layer is now encapsulated within the standard `Program.Main` method, adhering to the minimal API hosting model structure.
* **Data Seeding in Separate File**: Initial database seeding logic has been moved out of the `AppDbContext` and into a dedicated `ModelBuilderSeeder` class within the Infrastructure layer, improving separation of concerns. The `20250411142144_SeedData.cs` migration file also reflects this seeded data.
* **Table Names and Columns Follow Document**: Entity configurations explicitly define table names (e.g., `Project_Employee`) and column properties (like `MaxLength`, `IsRequired`, `HasColumnType`), indicating adherence to specified database schema documentation. Migrations reflect these configurations.

## How to Run the Project

1.  **Prerequisites**:
    * .NET 8 SDK
    * SQL Server (or SQL Server Express) instance.
2.  **Configuration**:
    * Open `Assignment2.Api/appsettings.json`.
    * Update the `DefaultConnection` string under `ConnectionStrings` to point to your SQL Server instance. Ensure the database name (`EFCoreAssignment2` by default) is suitable or change it as needed.
3.  **Database Setup**:
    * **Recommendation:** If you have a database from a previous version of this assignment (e.g., Assignment 1) with the same name (`EFCoreAssignment2`), it's recommended to **drop** it first to avoid any migration conflicts. You can do this using SQL Server Management Studio (SSMS) or a SQL command (`DROP DATABASE EFCoreAssignment2;`).
    * Open a terminal or command prompt in the `Assignment2.Infrastructure` directory.
    * Run the EF Core migrations to create the database and schema, and seed initial data:
        ```bash
        dotnet ef database update -p Assignment2.Infrastructure -s Assignment2.Api
        ```
        *(Ensure the startup project path is correct relative to your terminal location)*
4.  **Run the API**:
    * Navigate to the `Assignment2.Api` directory in your terminal.
    * Run the application:
        ```bash
        dotnet run
        ```
    * The API should now be running. You can access the Swagger UI (usually at `https://localhost:<port>/swagger` or `http://localhost:<port>/swagger` as defined in `launchSettings.json`) to interact with the endpoints.