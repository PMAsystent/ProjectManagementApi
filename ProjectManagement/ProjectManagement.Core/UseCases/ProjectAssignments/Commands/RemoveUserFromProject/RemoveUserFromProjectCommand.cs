using MediatR;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.RemoveUserFromProject
{
    public class RemoveUserFromProjectCommand : IRequest
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}