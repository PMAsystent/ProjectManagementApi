using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Queries.GetUnassignedToTaskUsers
{
    public class GetUnassignedToTaskUserAuthorizer : AbstractRequestAuthorizer<GetUnassignedToTaskUserQuery>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetUnassignedToTaskUserAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        
        public override void BuildPolicy(GetUnassignedToTaskUserQuery request)
        {
            UseRequirement(new AssignedToProjectRequirement()
            {
                TaskId = request.TaskId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
