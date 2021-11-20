using MediatR;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.UpdateProjectAssignment
{
    public class UpdateProjectAssignmentCommand : IRequest
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string MemberType { get; set; }
        public string ProjectRole { get; set; }   
    }
}