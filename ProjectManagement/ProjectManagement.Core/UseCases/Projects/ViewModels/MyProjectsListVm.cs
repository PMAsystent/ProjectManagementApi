using ProjectManagement.Core.UseCases.Projects.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Projects.ViewModels
{
    public class MyProjectsListVm
    {
        public ICollection<ProjectDto> ProjectsList { get; set; }
        public int Count { get; set; }
    }
}
