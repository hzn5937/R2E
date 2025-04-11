using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.EntityConfigurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salaries>
    {
        public void Configure(EntityTypeBuilder<Salaries> entity)
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.HasOne(s => s.Employee)
                .WithOne(e => e.Salary)
                .HasForeignKey<Salaries>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
