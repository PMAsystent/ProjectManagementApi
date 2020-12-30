using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class OverseeConfiguration : IEntityTypeConfiguration<Oversee>
    {
        public void Configure(EntityTypeBuilder<Oversee> builder)
        {
            builder.Property(o => o.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}