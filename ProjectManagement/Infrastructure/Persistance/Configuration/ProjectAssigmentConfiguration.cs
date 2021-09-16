using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class ProjectAssigmentConfiguration : IEntityTypeConfiguration<ProjectAssigment>
    {
        public void Configure(EntityTypeBuilder<ProjectAssigment> builder)
        {
        }
    }
}