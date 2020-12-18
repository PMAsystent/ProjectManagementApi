using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.DatabaseContext
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Projects.Any())
            {
                context.Projects.Add( new Domain.Entities.Project
                    {
                         Description="Strona",
                         IsActive = true
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
