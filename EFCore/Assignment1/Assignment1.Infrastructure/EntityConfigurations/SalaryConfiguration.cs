using Assignment1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment1.Infrastructure.EntityConfigurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> entity)
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.HasOne(s => s.Employee)
                .WithOne(e => e.Salary)
                .HasForeignKey<Salary>(s => s.EmployeeId);
        }
    }
}
