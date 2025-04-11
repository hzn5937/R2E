using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.JoinedDate)
                .IsRequired();

            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salaries>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            entity.HasMany(e => e.ProjectEmployees)
                .WithOne(pe => pe.Employee)
                .HasForeignKey(pe => pe.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
