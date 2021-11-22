using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskAuthorizer : AbstractRequestAuthorizer<UpdateTaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(UpdateTaskCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement()
            {
                StepId = request.StepId,
                UserId = _currentUserService.UserId
            });
        }
    }
}