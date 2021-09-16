using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class TaskAssigmentConfiguration : IEntityTypeConfiguration<TaskAssigment>
    {
        public void Configure(EntityTypeBuilder<TaskAssigment> builder)
        {
        }
    }
}