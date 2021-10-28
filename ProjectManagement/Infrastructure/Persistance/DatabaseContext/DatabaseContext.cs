using Domain.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using ProjectManagement.Core.Base.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace Infrastructure.Persistance.DatabaseContext
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IDomainEventService domainEventService) : base(options, operationalStoreOptions)
        {
            _domainEventService = domainEventService;
        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }

        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }

        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditableEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                    // TODO: Insert ID instead of name
                    ((AuditableEntity)entityEntry.Entity).CreatedBy =
                        _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "default";
                }
                else
                {
                    Entry((AuditableEntity)entityEntry.Entity)
                        .Property(p => p.Created)
                        .IsModified = false;
                    // TODO: Insert ID instead of name
                    Entry((AuditableEntity)entityEntry.Entity)
                        .Property(p => p.CreatedBy)
                        .IsModified = false;
                }

                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).LastModifiedBy =
                    _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "default";
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}