using Microsoft.EntityFrameworkCore;
using Assignment1.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment1.Infrastructure.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.JoinedDate)
                .IsRequired();

            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            entity.HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId);

            entity.HasMany(e => e.ProjectEmployees)
                .WithOne(pe => pe.Employee)
                .HasForeignKey(pe => pe.EmployeeId);
        }
    }
}
