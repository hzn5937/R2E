# Assignment 1 - EF Core Implementation

## Overview

This project serves as an assignment focusing on the implementation and understanding of Entity Framework Core (EF Core) within a .NET application. It demonstrates key EF Core concepts, including entity modeling, database context configuration, migrations, seeding, and the use of Fluent API for mapping.

## Architecture

The project follows the principles of Clean Architecture, separating concerns into distinct layers:

* **`Assignment1.Domain`**: Contains the core business entities and domain logic. This layer has no dependencies on other layers.
* **`Assignment1.Application`**: Orchestrates the application's use cases. It depends on the Domain layer but not on Infrastructure or Api layers. Further implementation for this layer will be implemented in Assignment 2.
* **`Assignment1.Infrastructure`**: Handles technical concerns like data access using EF Core. It includes the `DbContext`, entity configurations, and database migrations. This layer depends on the Domain and Application layers.
* **`Assignment1.Api`**: The presentation layer, implemented as a .NET Web API. It handles incoming HTTP requests and interacts with the Application layer. This is the startup project.

## EF Core Features Implemented

This project demonstrates several core EF Core features:

1.  **Entity Modeling & Relational Mapping**:
    * Domain entities (`Department`, `Employee`, `Project`, `Salary`, `ProjectEmployee`) are defined in the `Assignment1.Domain/Entities` folder.
    * Relationships are established between entities:
        * One-to-Many: `Department` to `Employee`.
        * One-to-One: `Employee` to `Salary`.
        * Many-to-Many: `Employee` to `Project` (using the `ProjectEmployee` join entity).

2.  **DbContext (`AppDbContext`)**:
    * Located in `Assignment1.Infrastructure/Data/AppDbContext.cs`.
    * Inherits from `Microsoft.EntityFrameworkCore.DbContext`.
    * Contains `DbSet<>` properties for each entity, allowing querying and saving data.

3.  **Configuration (Fluent API)**:
    * Entity configurations are defined using the Fluent API in separate configuration classes within the `Assignment1.Infrastructure/EntityConfigurations` folder (e.g., `DepartmentConfiguration`, `EmployeeConfiguration`).
    * These configurations specify details like primary keys, foreign keys, relationships, constraints (e.g., `MaxLength`), and column types.
    * Configurations are applied in the `AppDbContext.OnModelCreating` method.

4.  **Migrations**:
    * EF Core Migrations are used to manage database schema changes over time.
    * Migration files are stored in the `Assignment1.Infrastructure/Migrations` folder.
    * These allow for evolving the database schema in a controlled way as the model changes.

5.  **Seeding**:
    * Initial data for the `Departments` table is seeded using the `HasData` method within the `DepartmentConfiguration` class.

## Setup and Running Instructions

Follow these steps to set up and run the project:

1.  **Prerequisites**:
    * .NET 8.0 SDK (Verify with `dotnet --version`). The project targets `net8.0`.
    * SQL Server (or SQL Server Express) instance accessible.

2.  **Install EF Core Tools**:
    * If you haven't already, install the `dotnet-ef` global tool:
        ```bash
        dotnet tool install --global dotnet-ef
        ```

3.  **Configure Connection String**:
    * Open the `Assignment1.Api/appsettings.json` file.
    * Locate the `ConnectionStrings` section.
    * Modify the `DefaultConnection` string to point to your SQL Server instance and desired database name. The current default is:
        `"Server=localhost;Database=EFCoreAssignment2;Trusted_Connection=True;TrustServerCertificate=True"`. Adjust server name, database name, and authentication details as needed.

4.  **Apply Migrations**:
    * Open a terminal or command prompt in the root directory of the solution (where the `.sln` file is, or containing the project folders).
    * Run the following command to apply the migrations and create/update the database schema. This command targets the `Assignment1.Infrastructure` project (where `DbContext` and migrations reside) and uses `Assignment1.Api` as the startup project to read the connection string:
        ```bash
        dotnet ef database update -p Assignment1.Infrastructure -s Assignment1.Api
        ```

5.  **Run the API**:
    * **Note:** Since this assignment focuses solely on EF Core setup and configuration without specific business logic implemented yet, running the API might not be essential for verifying the EF Core aspects (which are primarily checked via migration application). However, the steps to run it are included below.
    * Navigate to the API project directory:
        ```bash
        cd Assignment1.Api
        ```
    * Run the API using the .NET CLI:
        ```bash
        dotnet run
        ```
    * The API will start, and you can access it via the URLs specified in `Properties/launchSettings.json` (e.g., `https://localhost:7211` or `http://localhost:5291`).
    * Swagger UI should be available at `/swagger` on the running instance (e.g., `https://localhost:7211/swagger`) for exploring the API endpoints.