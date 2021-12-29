using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Dto
{
    public class TaskAssignedUserDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<TaskAssignedUserDto, User>().ReverseMap();
        }
    }
}