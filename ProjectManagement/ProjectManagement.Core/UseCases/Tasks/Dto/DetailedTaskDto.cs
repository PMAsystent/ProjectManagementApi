using Domain.Entities;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Tasks.Dto
{
    public class DetailedTaskDto : IMapFrom<Task>
    {
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<DetailedTaskDto, Task>().ReverseMap();
        }
    }
}