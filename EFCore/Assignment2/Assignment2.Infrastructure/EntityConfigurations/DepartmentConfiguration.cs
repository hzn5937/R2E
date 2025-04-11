using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> entity)
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
