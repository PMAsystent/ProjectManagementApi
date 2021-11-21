using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Steps.Commands.DeleteStep
{
    public class DeleteStepAuthorizer : AbstractRequestAuthorizer<DeleteStepCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteStepAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(DeleteStepCommand request)
        {
            UseRequirement(new SuperMemberRequirement()
            {
                StepId = request.StepId,
                UserId = _currentUserService.UserId
            });
        }
    }
}