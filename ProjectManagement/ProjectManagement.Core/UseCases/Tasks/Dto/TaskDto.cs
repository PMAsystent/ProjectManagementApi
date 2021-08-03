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
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public bool IsActive { get; set; }
        public int StepId { get; set; }
        
        public ICollection<Oversee> Oversees { get; set; }
        public ICollection<Assign> Assigns { get; set; }
        public ICollection<TaskChange> TaskChanges { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Task, TaskDto>().ReverseMap();
        }
        
        
    }
}
