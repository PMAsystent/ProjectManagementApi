using System.Collections.Generic;
using Domain.Entities;

namespace ProjectManagement.Core.UseCases.Projects.Utils
{
    public interface IProjectsPercentageService
    {
        int GetProgressPercentageForProject(List<Step> stepsInProject);
    }
}