using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskStatus
{
    public class UpdateSubtaskStatusAuthorizer : AbstractRequestAuthorizer<UpdateSubtaskStatusCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateSubtaskStatusAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(UpdateSubtaskStatusCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement
            {
                SubtaskId = request.SubtaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}