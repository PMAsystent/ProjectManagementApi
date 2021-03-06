using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects
{
    public class ProjectVm
    {
        public IList<ProjectDto> ProjectList { get; set; }
    }
}
