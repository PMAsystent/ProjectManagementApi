using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Dto
{
    public class TaskDto : IMapFrom<Task>
    {
        [JsonProperty(Order = int.MinValue)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public bool IsActive { get; set; }
        public int StepId { get; set; }
        
        public ICollection<TaskAssignment> TaskAssigments { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Task, TaskDto>().ReverseMap();
        }
        
        
    }
}
