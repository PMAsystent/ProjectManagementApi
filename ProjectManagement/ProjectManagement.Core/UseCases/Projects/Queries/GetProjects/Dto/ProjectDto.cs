using Domain.Entities;
using Newtonsoft.Json;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects.Dto
{
    public class ProjectDto : IMapFrom<Project>
    {
        [JsonProperty(Order = int.MinValue)]
        public int Id { get; set; }

        [JsonProperty(Order = int.MinValue)]
        public string Description { get; set; }

        [JsonProperty(Order = int.MinValue)]
        public bool IsActive { get; set; }

        // TODO:
        // Check on AdobeXD project, which properties should be here.

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Project, ProjectDto>();
        }
    }
}
