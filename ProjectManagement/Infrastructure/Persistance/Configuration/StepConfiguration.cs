using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(30);
            builder.Property(s => s.Description)
                .HasMaxLength(100);

            builder
                .HasMany(s => s.Tasks)
                .WithOne(t => t.Step)
                .HasForeignKey(t => t.StepId);
        }
    }
}
