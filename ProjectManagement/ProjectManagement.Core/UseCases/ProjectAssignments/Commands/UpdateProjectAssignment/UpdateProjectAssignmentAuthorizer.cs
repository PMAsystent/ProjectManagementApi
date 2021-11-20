using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.UpdateProjectAssignment
{
    public class UpdateProjectAssignmentAuthorizer : AbstractRequestAuthorizer<UpdateProjectAssignmentCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateProjectAssignmentAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(UpdateProjectAssignmentCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}