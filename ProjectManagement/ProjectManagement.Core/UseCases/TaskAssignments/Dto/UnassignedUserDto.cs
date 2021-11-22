using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Dto
{
    public class UnassignedUserDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<User, UnassignedUserDto>().ReverseMap();
        }
    }
}
