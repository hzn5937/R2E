﻿// <auto-generated />
using System;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment2.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250410041504_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assignment2.Domain.Entities.Departments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Software Development"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Finance"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Accountant"
                        },
                        new
                        {
                            Id = 4,
                            Name = "HR"
                        });
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Employees", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("JoinedDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.ProjectEmployees", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.HasKey("ProjectId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Project_Employee", (string)null);
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Projects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Salaries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Employees", b =>
                {
                    b.HasOne("Assignment2.Domain.Entities.Departments", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.ProjectEmployees", b =>
                {
                    b.HasOne("Assignment2.Domain.Entities.Employees", "Employee")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment2.Domain.Entities.Projects", "Project")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Salaries", b =>
                {
                    b.HasOne("Assignment2.Domain.Entities.Employees", "Employee")
                        .WithOne("Salary")
                        .HasForeignKey("Assignment2.Domain.Entities.Salaries", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Departments", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Employees", b =>
                {
                    b.Navigation("ProjectEmployees");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("Assignment2.Domain.Entities.Projects", b =>
                {
                    b.Navigation("ProjectEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
