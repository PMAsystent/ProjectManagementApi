using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask
{
    public class CreateSubtaskCommand : IRequest<CreateSubtaskCommandResponse>, IMapFrom<Subtask>
    {
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public int TaskId { get; set; }
        
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateSubtaskCommand, Subtask>();
        }
    }
}