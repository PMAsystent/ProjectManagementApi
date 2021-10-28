using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Persistance.DatabaseContext
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // if (!context.Projects.Any())
            // {
            //     context.Projects.Add(new()
            //     {
            //         Name = "Project1",
            //         Description = "Test project",
            //         DueDate = DateTime.UtcNow.AddMonths(1),
            //         IsActive = true,
            //         Steps = new List<Step>()
            //         {
            //             new()
            //             {
            //                 Name = "Step",
            //                 Description = "Test step",
            //                 StartDate = DateTime.UtcNow,
            //                 EndDate = DateTime.UtcNow.AddDays(14),
            //                 TargetDate = DateTime.UtcNow.AddDays(14),
            //                 IsActive = true,
            //                 Tasks = new List<Domain.Entities.Task>()
            //                 {
            //                     new()
            //                     {
            //                         Name = "Task#1",
            //                         Description = "Test task",
            //                         Priority = TaskPriority.Level0,
            //                         TaskStatus = TaskStatus.Start,
            //                         StartDate = DateTime.UtcNow,
            //                         EndDate = DateTime.UtcNow.AddDays(14),
            //                         DueDate = DateTime.UtcNow.AddDays(14),
            //                     }
            //                     },
            //                 }
            //             },
            //         });
            //     await context.SaveChangesAsync();
            // }
        }
    }
}
