using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Users.Queries.FindUserInProject
{
    public class FindUserInProjectAuthorizer : AbstractRequestAuthorizer<FindUserInProjectQuery>
    {
        private readonly ICurrentUserService _currentUserService;
        
        public FindUserInProjectAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(FindUserInProjectQuery request)
        {
            UseRequirement(new AssignedToProjectRequirement()
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}