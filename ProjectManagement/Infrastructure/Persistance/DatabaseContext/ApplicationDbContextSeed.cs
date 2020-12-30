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
            // Seed, if necessary
            if (!context.Projects.Any())
            {
                context.Projects.Add(new()
                {
                    Description = "Test project",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    TargetDate = DateTime.UtcNow.AddMonths(1).AddDays(7),
                    IsActive = true,
                    Steps = new List<Step>()
                    {
                        new()
                        {
                            Description = "First step",
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddDays(14),
                            TargetDate = DateTime.UtcNow.AddDays(14),
                            IsActive = true,
                            Tasks = new List<Domain.Entities.Task>()
                            {
                                new()
                                {
                                    Description = "Task#1",
                                    Priority = Priority.Level0,
                                    Status = Status.Start,
                                    StartDate = DateTime.UtcNow,
                                    EndDate = DateTime.UtcNow.AddDays(14),
                                    TargetDate = DateTime.UtcNow.AddDays(14),
                                    Oversees = new List<Oversee>()
                                    {
                                        new ()
                                        {
                                            StartDate = DateTime.UtcNow,
                                            EndDate = DateTime.UtcNow.AddDays(14),
                                            ProjectManager = new ()
                                            {
                                                Name = "Jan",
                                                Surname = "Kowalski",
                                                PhoneNumber = "123123123",
                                                Email = "jan@kowalski.com",
                                                Boss = new ()
                                                {
                                                    Name = "Andrzej",
                                                    Surname = "Nowak",
                                                    PhoneNumber = "123123123",
                                                    Email = "andrzej@nowak.com",
                                                }
                                            }
                                        }
                                    }
                                    
                                },
                            }
                        },
                    }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
