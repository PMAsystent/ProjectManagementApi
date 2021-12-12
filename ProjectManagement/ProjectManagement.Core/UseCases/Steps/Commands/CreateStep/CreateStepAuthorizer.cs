using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Steps.Commands.CreateStep
{
    public class CreateStepAuthorizer : AbstractRequestAuthorizer<CreateStepCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public CreateStepAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(CreateStepCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}