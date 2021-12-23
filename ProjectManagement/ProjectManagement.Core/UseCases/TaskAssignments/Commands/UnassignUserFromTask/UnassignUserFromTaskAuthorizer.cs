using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Commands.UnassignUserFromTask
{
    public class UnassignUserFromTaskAuthorizer : AbstractRequestAuthorizer<UnassignUserFromTaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public UnassignUserFromTaskAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(UnassignUserFromTaskCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement()
            {
                TaskId = request.TaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
