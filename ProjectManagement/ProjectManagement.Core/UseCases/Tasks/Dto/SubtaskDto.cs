using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Dto
{
    public class SubtaskDto : IMapFrom<Subtask>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int TaskId { get; set; }

        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<SubtaskDto, Subtask>().ReverseMap();
        }
    }
}