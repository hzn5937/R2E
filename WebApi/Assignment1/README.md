# Task Management Web API (Assignment1)

## Overview

This project is a simple ASP.NET Core Web API designed for managing tasks, following the principles of **Clean Architecture**. It provides basic CRUD (Create, Read, Update, Delete) operations for task items, along with bulk operations for creating and deleting multiple tasks. The API utilizes an in-memory repository for data storage, making it easy to run and test without external database dependencies.

## Core Functions

The primary function of this API is to manage a list of tasks. Key functionalities include:

* **Get All Tasks:** Retrieves a list of all task items.
* **Get Task by ID:** Retrieves a specific task item by its unique identifier (GUID).
* **Create Task:** Adds a new task item to the list. It prevents duplicate tasks based on the title.
* **Update Task:** Modifies an existing task item's title or completion status. It also prevents duplicate tasks based on the title during updates.
* **Delete Task:** Removes a task item from the list by its ID.
* **Bulk Create Tasks:** Adds multiple new task items in a single request. It filters out duplicate titles within the request and also checks against existing tasks.
* **Bulk Delete Tasks:** Removes multiple task items by their IDs in a single request.

## Project Structure & Components

The project follows **Clean Architecture**, separating concerns into distinct layers:

* **Assignment1.Domain:** The core layer, defining entities (`TaskItem`) and repository interfaces (`ITaskRepository`). This layer has no dependencies on other layers.
* **Assignment1.Application:** The application layer, containing business logic (`TaskService`), DTOs, mapping profiles (`TaskProfile`), service interfaces (`ITaskService`), and application-specific extensions/exceptions (`ConflictException`). It depends on the Domain layer.
* **Assignment1.Infrastructure:** The infrastructure layer, implementing data access (`TaskRepository`) and potentially other external concerns like data seeding (`TaskDataSeeder`). It depends on the Application layer.
* **Assignment1.Api:** The presentation layer (Web API), responsible for handling HTTP requests, routing (`TaskController`), API-specific configurations (`Program.cs`, Converters, Middleware), and interacting with the Application layer. It depends on the Application layer.

Here's an explanation of the specific components you asked about:

* **`Assignment1.Api.Converters`**
    * Contains custom JSON converters (`BooleanJsonConverter`, `GuidJsonConverter`).
    * These ensure that boolean values are strictly `true` or `false` and that GUIDs are correctly parsed from valid string representations during JSON deserialization.
    * They also **customize error messages** by throwing specific `JsonException`s with detailed messages when the input format or type is incorrect (e.g., non-boolean for `isCompleted`, non-GUID string for IDs), providing clearer feedback than default framework errors. *Note: Providing invalid input types for these fields might pause the application during debugging as the converter throws an exception.*
* **`Assignment1.Api.Extensions`**
    * Includes `ServiceCollectionExtensions`.
    * This class uses extension methods to centralize the registration of application services (like `ITaskService`, `ITaskRepository`, and AutoMapper) into the dependency injection container, keeping `Program.cs` cleaner.
* **`Assignment1.Api.Middlewares`**
    * Features the `ExceptionMiddleware`.
    * This middleware provides global exception handling. It catches unhandled exceptions, logs them, and returns standardized JSON error responses to the client based on the exception type (e.g., 400 Bad Request for `JsonException` or `ArgumentException`, 409 Conflict for `ConflictException`, 500 Internal Server Error for others).
* **Customizations in `Program.cs`** 
    * **Service Registration:** Calls `AddApplicationServices()` extension method to register application-specific services defined in `Assignment1.Api.Extensions`.
    * **JSON Converters:** Configures JSON options (`AddJsonOptions`) to use the custom `BooleanJsonConverter` and `GuidJsonConverter` for improved input handling and error reporting.
    * **Middleware Registration:** Registers the `ExceptionMiddleware` early in the pipeline (`app.UseMiddleware<ExceptionMiddleware>()`) to catch exceptions globally.
    * **Swagger/OpenAPI:** Configures Swagger for API documentation and testing UI, enabled during development.
* **`Assignment1.Application.DTOs`**
    * Contains Data Transfer Objects (`TaskCreateDto`, `TaskItemDto`, `TaskUpdateDto`).
    * These define the structure of data sent to and received from the API endpoints, decoupling the API contract from the internal domain entities. `TaskCreateDto` and `TaskUpdateDto` include validation attributes like `[Required]`.
* **`Assignment1.Application.Extensions`**
    * Contains the `ConflictException` class.
    * This is a **custom exception class that inherits directly from the base `System.Exception` class**. It is thrown specifically when an operation (like creating or updating a task) would result in a duplicate task title, allowing the `ExceptionMiddleware` to identify this specific scenario and return a meaningful 409 Conflict status code.
* **`Assignment1.Application.Mapping`**
    * Contains AutoMapper profiles (`TaskProfile`).
    * This configures the mapping between `TaskItem` domain entities and the various `Task...Dto` objects using AutoMapper, simplifying the transformation of data between layers.

## How to Run the Project

1.  **Prerequisites:**
    * .NET 8 SDK (or the version specified in the project files)
2.  **Clone/Download:** Obtain the project code.
3.  **Navigate:** Open a terminal or command prompt and navigate to the `WebApi/Assignment1.Api` directory.
4.  **Run:** Execute the following command:
    ```bash
    dotnet run
    ```
5.  **Access API:**
    * The API will typically launch on `https://localhost:7176` and `http://localhost:5133` (check the console output and `Properties/launchSettings.json` for the exact URLs).
    * Open a web browser and navigate to `/swagger` on one of the running URLs (e.g., `https://localhost:7176/swagger`) to access the Swagger UI for testing the endpoints.

## Other Points

* **In-Memory Data:** The `TaskRepository` uses a `ConcurrentDictionary` for storing tasks in memory. Data will be lost when the application stops. This is seeded with initial data from `TaskDataSeeder`.
* **Bulk Operations:** The API supports creating and deleting multiple tasks efficiently via the `/api/Tasks` endpoints (note the different route compared to single-item operations).