using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class TaskAssigmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
    {
        public void Configure(EntityTypeBuilder<TaskAssignment> builder)
        {
        }
    }
}