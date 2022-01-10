using System.Collections.Generic;
using Domain.Entities;

namespace ProjectManagement.Core.Base.Utils
{
    public interface IProgressPercentageService
    {
        int GetProgressPercentageForProject(List<Step> stepsInProject);
        int GetProgressPercentageForStep(List<Task> tasksInStep);
        int GetProgressPercentageForTask(Task task);
    }
}