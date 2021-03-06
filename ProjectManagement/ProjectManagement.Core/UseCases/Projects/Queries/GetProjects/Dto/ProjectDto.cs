using Domain.Entities;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects.Dto
{
    public class ProjectDto : IMapFrom<Project>
    {
        [JsonProperty(Order = int.MinValue)]
        public int ID { get; set; }

        [JsonProperty(Order = int.MinValue)]
        public string Description { get; set; }

        [JsonProperty(Order = int.MinValue)]
        public bool IsActive { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Project, ProjectDto>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive));
        }
    }
}
