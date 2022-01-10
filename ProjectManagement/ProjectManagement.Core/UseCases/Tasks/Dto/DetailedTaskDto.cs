using System;
using System.Collections.Generic;
using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Dto
{
    public class DetailedTaskDto : IMapFrom<Task>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public int ProgressPercentage { get; set; }

        public int StepId { get; set; }
        public ICollection<TaskAssignedUserDto> AssignedUser { get; set; }
        public ICollection<SubtaskDto> Subtasks { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<DetailedTaskDto, Task>().ReverseMap();
        }
    }
}