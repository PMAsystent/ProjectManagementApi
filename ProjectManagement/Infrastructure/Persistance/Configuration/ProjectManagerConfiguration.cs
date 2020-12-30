using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class ProjectManagerConfiguration : IEntityTypeConfiguration<ProjectManager>
    {
        public void Configure(EntityTypeBuilder<ProjectManager> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Surname)
                .IsRequired();

            builder
                .HasMany(p => p.Oversee)
                .WithOne(o => o.ProjectManager)
                .HasForeignKey(o => o.ProjectManagerId);

            builder
                .HasMany(p => p.Assign)
                .WithOne(a => a.ProjectManager)
                .HasForeignKey(a => a.ProjectManagerId);
        }
    }
}
