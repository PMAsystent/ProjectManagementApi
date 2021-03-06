using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(d => d.Description)
                .HasMaxLength(100);

            builder
                .HasMany(p => p.Steps)
                .WithOne(s => s.Project)
                .HasForeignKey(s => s.ProjectId);

        }
    }
}
