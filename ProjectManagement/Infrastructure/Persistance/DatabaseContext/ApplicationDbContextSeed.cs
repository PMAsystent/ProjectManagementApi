using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Persistance.DatabaseContext
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Projects.Any())
            {
                context.Projects.Add(new()
                {
                    Name = "Project1",
                    Description = "Test project",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    TargetDate = DateTime.UtcNow.AddMonths(1).AddDays(7),
                    IsActive = true,
                    Steps = new List<Step>()
                    {
                        new()
                        {
                            Name = "Step",
                            Description = "Test step",
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddDays(14),
                            TargetDate = DateTime.UtcNow.AddDays(14),
                            IsActive = true,
                            Tasks = new List<Domain.Entities.Task>()
                            {
                                new()
                                {
                                    Name = "Task#1",
                                    Description = "Test task",
                                    Priority = Priority.Level0,
                                    Status = Status.Start,
                                    StartDate = DateTime.UtcNow,
                                    EndDate = DateTime.UtcNow.AddDays(14),
                                    TargetDate = DateTime.UtcNow.AddDays(14),
                                }
                                },
                            }
                        },
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
