using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Departments> Departments => Set<Departments>();
        public DbSet<Employees> Employees => Set<Employees>();
        public DbSet<Projects> Projects => Set<Projects>();
        public DbSet<Salaries> Salaries => Set<Salaries>();
        public DbSet<ProjectEmployees> ProjectEmployees => Set<ProjectEmployees>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EntityConfigurations.DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ProjectEmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.SalaryConfiguration());

            ModelBuilderSeeder.Seed(modelBuilder);
        }
    }
}
