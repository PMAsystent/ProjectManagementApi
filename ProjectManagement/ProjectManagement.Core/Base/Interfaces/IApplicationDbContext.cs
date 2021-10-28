using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;


namespace ProjectManagement.Core.Base.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }

        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }

        public DbSet<User> Users { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}