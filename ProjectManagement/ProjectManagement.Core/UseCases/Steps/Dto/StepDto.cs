using Domain.Entities;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;
using System;

namespace ProjectManagement.Core.UseCases.Steps.Dto
{
    public class StepDto : IMapFrom<Step>
    {
        [JsonProperty(Order = int.MinValue)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Step, StepDto>().ReverseMap();
        }
    }
}
