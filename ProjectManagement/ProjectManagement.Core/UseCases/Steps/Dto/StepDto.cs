using Domain.Entities;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.UseCases.Steps.Dto
{
    public class StepDto : IMapFrom<Step>
    {
        [JsonProperty(Order = int.MinValue)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public int ProjectId { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Step, StepDto>();
        }
    }
}
