using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace ProjectManagement.Core.UseCases.Projects.Utils
{
    public class ProjectsPercentageService : IProjectsPercentageService
    {
        public int GetProgressPercentageForProject(List<Step> stepsInProject)
        {
            var allTasksInProject = new List<Task>();
            
            if (stepsInProject.Count == 0)
            {
                return 0;
            }

            var stepsWithTasks = stepsInProject.Where(step => step.Tasks != null).ToList();
            foreach (var step in stepsWithTasks)
            {
                allTasksInProject.AddRange(step.Tasks);
            }

            if (allTasksInProject.Count == 0)
            {
                return 0;
            }

            return allTasksInProject.Count(t => t.TaskStatus == TaskStatus.Done.ToString()) * 100 /
                   allTasksInProject.Count;
        }
    }
}