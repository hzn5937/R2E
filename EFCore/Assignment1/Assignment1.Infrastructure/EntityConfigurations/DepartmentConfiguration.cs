using Assignment1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Assignment1.Infrastructure.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            entity.HasData(
                new Department { Id = 1, Name = "Software Development" },
                new Department { Id = 2, Name = "Finance" },
                new Department { Id = 3, Name = "Accountant" },
                new Department { Id = 4, Name = "HR" }
            );
        }
    }
}
