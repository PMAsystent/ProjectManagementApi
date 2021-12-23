using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask
{
    public class AssignUserToTaskAuthorizer : AbstractRequestAuthorizer<AssignUserToTaskCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public AssignUserToTaskAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(AssignUserToTaskCommand request)
        {
            UseRequirement(new AssignedToProjectRequirement()
            {
                TaskId = request.TaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
