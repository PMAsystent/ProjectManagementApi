﻿using Domain.Entities;
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
        public DbSet<TaskAssigment> TaskAssigments { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<ProjectAssigment> ProjectAssigments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
