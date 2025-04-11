using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Assignment2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "JoinedDate", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2023, 5, 10), "Alice Johnson" },
                    { 2, 2, new DateOnly(2024, 1, 12), "Bob Smith" },
                    { 3, 1, new DateOnly(2023, 8, 5), "Charlie Lee" },
                    { 4, 3, new DateOnly(2024, 2, 1), "Diana Patel" },
                    { 5, 4, new DateOnly(2023, 9, 18), "Ethan Brown" },
                    { 6, 1, new DateOnly(2023, 11, 23), "Fiona Davis" },
                    { 7, 2, new DateOnly(2024, 3, 2), "George Wang" },
                    { 8, 3, new DateOnly(2024, 1, 25), "Hannah Kim" },
                    { 9, 4, new DateOnly(2023, 7, 14), "Ian Walker" },
                    { 10, 1, new DateOnly(2024, 2, 14), "Julia Martinez" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Project Alpha" },
                    { 2, "Project Beta" },
                    { 3, "Project Gamma" },
                    { 4, "Project Delta" },
                    { 5, "Project Epsilon" }
                });

            migrationBuilder.InsertData(
                table: "Project_Employee",
                columns: new[] { "EmployeeId", "ProjectId", "Enable" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 2, 1, true },
                    { 3, 1, false },
                    { 4, 2, true },
                    { 5, 2, true },
                    { 6, 2, false },
                    { 7, 3, true },
                    { 8, 3, false },
                    { 1, 4, true },
                    { 6, 4, true },
                    { 9, 4, false },
                    { 2, 5, false },
                    { 3, 5, true },
                    { 5, 5, true },
                    { 10, 5, true }
                });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "Id", "EmployeeId", "Salary" },
                values: new object[,]
                {
                    { 1, 1, 60000m },
                    { 2, 2, 70000m },
                    { 3, 3, 65000m },
                    { 4, 4, 80000m },
                    { 5, 5, 75000m },
                    { 6, 6, 62000m },
                    { 7, 7, 72000m },
                    { 8, 8, 68000m },
                    { 9, 9, 64000m },
                    { 10, 10, 71000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "Project_Employee",
                keyColumns: new[] { "EmployeeId", "ProjectId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Salaries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
