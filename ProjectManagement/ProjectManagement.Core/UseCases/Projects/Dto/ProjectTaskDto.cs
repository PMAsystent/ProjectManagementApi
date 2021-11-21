using System;
using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class ProjectTaskDto : IMapFrom<Task>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public int ProgressPercentage { get; set; }
        public int StepId { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Task, ProjectTaskDto>().ReverseMap();
        }
    }
}