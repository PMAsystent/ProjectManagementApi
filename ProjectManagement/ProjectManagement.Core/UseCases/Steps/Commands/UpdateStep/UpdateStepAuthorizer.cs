using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepAuthorizer : AbstractRequestAuthorizer<UpdateStepCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateStepAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(UpdateStepCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                StepId = request.Id,
                UserId = _currentUserService.UserId
            });
        }
    }
}