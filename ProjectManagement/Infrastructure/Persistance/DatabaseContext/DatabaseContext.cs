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


        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; } 
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskAssigment> TaskAssigments { get; set; }
        public DbSet<ProjectAssigment> ProjectAssigments { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        
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
                    ((AuditableEntity)entityEntry.Entity).CreatedBy =
                        _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "SuperAdmin";
                }
                else
                {
                    Entry((AuditableEntity)entityEntry.Entity)
                        .Property(p => p.Created)
                        .IsModified = false;
                    Entry((AuditableEntity)entityEntry.Entity)
                        .Property(p => p.CreatedBy)
                        .IsModified = false;
                }

                ((AuditableEntity)entityEntry.Entity).LastModified = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).LastModifiedBy =
                    _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "SuperAdmin";
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
