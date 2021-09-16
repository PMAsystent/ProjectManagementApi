using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(30);
            builder.Property(p => p.Description)
                .HasMaxLength(100);

            builder
                .HasMany(p => p.Steps)
                .WithOne(s => s.Project)
                .HasForeignKey(s => s.ProjectId);
            
            builder
                .HasMany(p => p.Assigns)
                .WithOne(s => s.Project)
                .HasForeignKey(s => s.ProjectId);

        }
    }
}
