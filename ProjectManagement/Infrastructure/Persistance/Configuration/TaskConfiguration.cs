using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(30);
            builder.Property(t => t.Description)
                .HasMaxLength(100);

            builder
                .HasMany(t => t.Assigns)
                .WithOne(a => a.Task)
                .HasForeignKey(a => a.TaskId);
        }
    }
}
