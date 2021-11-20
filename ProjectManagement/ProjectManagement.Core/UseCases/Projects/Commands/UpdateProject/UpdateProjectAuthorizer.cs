using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectAuthorizer : AbstractRequestAuthorizer<UpdateProjectCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateProjectAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(UpdateProjectCommand request)
        {
            UseRequirement(new SuperMemberRequirement
            {
                ProjectId = request.Id,
                UserId = _currentUserService.UserId
            });
        }
    }
}
