using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Mappings;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask
{
    public class AssignUserToTaskCommand : IRequest, IMapFrom<TaskAssignment>
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<AssignUserToTaskCommand, TaskAssignment>();
        }
    }
}
