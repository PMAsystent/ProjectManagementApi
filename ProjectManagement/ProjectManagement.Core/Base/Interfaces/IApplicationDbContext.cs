using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Base.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Oversee> Oversees { get; set; }
        public DbSet<Assign> Assigns { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
