using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class TaskChangeConfiguration : IEntityTypeConfiguration<TaskChange>
    {
        public void Configure(EntityTypeBuilder<TaskChange> builder)
        {
            builder.Property(tc => tc.Status)
                .IsRequired();

            builder.Property(tc => tc.ChangeDescription)
                .IsRequired();
        }
    }
}