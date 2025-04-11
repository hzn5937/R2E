using Assignment2.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.EntityConfigurations
{
    public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployees>
    {
        public void Configure(EntityTypeBuilder<ProjectEmployees> entity)
        {
            entity.ToTable("Project_Employee");

            entity.HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            entity.HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
