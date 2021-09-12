using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class AssignConfiguration : IEntityTypeConfiguration<Assign>
    {
        public void Configure(EntityTypeBuilder<Assign> builder)
        {
        }
    }
}