using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class BossConfiguration : IEntityTypeConfiguration<Boss>
    {
        public void Configure(EntityTypeBuilder<Boss> builder)
        {
            builder
                .HasMany(b => b.ProjectManagers)
                .WithOne(p => p.Boss)
                .HasForeignKey(p => p.BossId);
        }
    }
}
