using Domain.Entities;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class ProjectDto : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; }
        public int ProgressPercentage { get; set; }
        public int ActiveTasksCount { get; set; }

        public ICollection<ProjectStepDto> Steps { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
