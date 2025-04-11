using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Data
{
    public static class ModelBuilderSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>().HasData(
                new Departments { Id = 1, Name = "Software Development" },
                new Departments { Id = 2, Name = "Finance" },
                new Departments { Id = 3, Name = "Accountant" },
                new Departments { Id = 4, Name = "HR" }
            );

            modelBuilder.Entity<Employees>().HasData(
                new Employees { Id = 1, Name = "Alice Johnson", DepartmentId = 1, JoinedDate = new DateOnly(2023, 5, 10) },
                new Employees { Id = 2, Name = "Bob Smith", DepartmentId = 2, JoinedDate = new DateOnly(2024, 1, 12) },
                new Employees { Id = 3, Name = "Charlie Lee", DepartmentId = 1, JoinedDate = new DateOnly(2023, 8, 5) },
                new Employees { Id = 4, Name = "Diana Patel", DepartmentId = 3, JoinedDate = new DateOnly(2024, 2, 1) },
                new Employees { Id = 5, Name = "Ethan Brown", DepartmentId = 4, JoinedDate = new DateOnly(2023, 9, 18) },
                new Employees { Id = 6, Name = "Fiona Davis", DepartmentId = 1, JoinedDate = new DateOnly(2023, 11, 23) },
                new Employees { Id = 7, Name = "George Wang", DepartmentId = 2, JoinedDate = new DateOnly(2024, 3, 2) },
                new Employees { Id = 8, Name = "Hannah Kim", DepartmentId = 3, JoinedDate = new DateOnly(2024, 1, 25) },
                new Employees { Id = 9, Name = "Ian Walker", DepartmentId = 4, JoinedDate = new DateOnly(2023, 7, 14) },
                new Employees { Id = 10, Name = "Julia Martinez", DepartmentId = 1, JoinedDate = new DateOnly(2024, 2, 14) }
            );

            modelBuilder.Entity<Salaries>().HasData(
                new Salaries { Id = 1, EmployeeId = 1, Salary = 60000 },
                new Salaries { Id = 2, EmployeeId = 2, Salary = 70000 },
                new Salaries { Id = 3, EmployeeId = 3, Salary = 65000 },
                new Salaries { Id = 4, EmployeeId = 4, Salary = 80000 },
                new Salaries { Id = 5, EmployeeId = 5, Salary = 75000 },
                new Salaries { Id = 6, EmployeeId = 6, Salary = 62000 },
                new Salaries { Id = 7, EmployeeId = 7, Salary = 72000 },
                new Salaries { Id = 8, EmployeeId = 8, Salary = 68000 },
                new Salaries { Id = 9, EmployeeId = 9, Salary = 64000 },
                new Salaries { Id = 10, EmployeeId = 10, Salary = 71000 }
            );

            modelBuilder.Entity<Projects>().HasData(
                new Projects { Id = 1, Name = "Project Alpha" },
                new Projects { Id = 2, Name = "Project Beta" },
                new Projects { Id = 3, Name = "Project Gamma"},
                new Projects { Id = 4, Name = "Project Delta" },
                new Projects { Id = 5, Name = "Project Epsilon" }
            );

            modelBuilder.Entity<ProjectEmployees>().HasData(
                new ProjectEmployees { ProjectId = 1, EmployeeId = 1, Enable = true },
                new ProjectEmployees { ProjectId = 1, EmployeeId = 2, Enable = true },
                new ProjectEmployees { ProjectId = 1, EmployeeId = 3, Enable = false },

                new ProjectEmployees { ProjectId = 2, EmployeeId = 4, Enable = true },
                new ProjectEmployees { ProjectId = 2, EmployeeId = 5, Enable = true },
                new ProjectEmployees { ProjectId = 2, EmployeeId = 6, Enable = false },

                new ProjectEmployees { ProjectId = 3, EmployeeId = 7, Enable = true },
                new ProjectEmployees { ProjectId = 3, EmployeeId = 8, Enable = false },

                new ProjectEmployees { ProjectId = 4, EmployeeId = 1, Enable = true },
                new ProjectEmployees { ProjectId = 4, EmployeeId = 6, Enable = true },
                new ProjectEmployees { ProjectId = 4, EmployeeId = 9, Enable = false },

                new ProjectEmployees { ProjectId = 5, EmployeeId = 10, Enable = true },
                new ProjectEmployees { ProjectId = 5, EmployeeId = 3, Enable = true },
                new ProjectEmployees { ProjectId = 5, EmployeeId = 2, Enable = false },
                new ProjectEmployees { ProjectId = 5, EmployeeId = 5, Enable = true }
            );
        }
    }
}
