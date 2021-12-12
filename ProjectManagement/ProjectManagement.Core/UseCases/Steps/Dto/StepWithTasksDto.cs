using Domain.Entities;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Steps.Dto
{
    public class StepWithTasksDto : IMapFrom<Step>
    {
        [JsonProperty(Order = int.MinValue)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProjectId { get; set; }
        
        public ICollection<StepTaskDto> Tasks { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Step, StepWithTasksDto>().ReverseMap();
        }
    }
}
