using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class ProjectAssigmentConfiguration : IEntityTypeConfiguration<ProjectAssignment>
    {
        public void Configure(EntityTypeBuilder<ProjectAssignment> builder)
        {
        }
    }
}