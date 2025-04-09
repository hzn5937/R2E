using Assignment1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Salary> Salaries => Set<Salary>();
        public DbSet<ProjectEmployee> ProjectEmployees => Set<ProjectEmployee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EntityConfigurations.DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ProjectEmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.SalaryConfiguration());
        }
    }
}
