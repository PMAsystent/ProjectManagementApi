using MediatR.Behaviors.Authorization;
using ProjectManagement.Core.Authorization;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectWithDetails
{
    public class GetProjectWithDetailsAuthorizer : AbstractRequestAuthorizer<GetProjectWithDetailsQuery>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetProjectWithDetailsAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public override void BuildPolicy(GetProjectWithDetailsQuery request)
        {
            UseRequirement(new AssignedToProjectRequirement
            {
                ProjectId = request.ProjectId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
