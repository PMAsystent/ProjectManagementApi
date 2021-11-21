using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Commands.ArchiveOrUnArchiveProject;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.RemoveUserFromProject
{
    public class RemoveUserFromProjectAuthorizer: AbstractRequestAuthorizer<RemoveUserFromProjectCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public RemoveUserFromProjectAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(RemoveUserFromProjectCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}