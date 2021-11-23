using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Dto
{
    public class TaskAssignemntDto : IMapFrom<TaskAssignment>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<TaskAssignemntDto, TaskAssignment>().ReverseMap();
        }
    }
}