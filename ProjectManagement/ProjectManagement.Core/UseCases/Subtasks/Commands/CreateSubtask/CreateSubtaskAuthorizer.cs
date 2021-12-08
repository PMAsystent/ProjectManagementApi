using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask
{
    public class CreateStepAuthorizer : AbstractRequestAuthorizer<CreateSubtaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public CreateStepAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(CreateSubtaskCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement
            {
                TaskId = request.TaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}