using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Projects.Dto
{
    public class ProjectStepDto : IMapFrom<Step>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgressPercentage { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Step, ProjectStepDto>().ReverseMap();
        }
    }
}