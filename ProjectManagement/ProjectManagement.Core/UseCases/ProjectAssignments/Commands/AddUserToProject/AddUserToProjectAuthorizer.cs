using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.AddUserToProject
{
    public class AddUserToProjectAuthorizer : AbstractRequestAuthorizer<AddUserToProjectCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public AddUserToProjectAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(AddUserToProjectCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}