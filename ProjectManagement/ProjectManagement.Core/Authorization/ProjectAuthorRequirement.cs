using System.Threading;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Authorization
{
    public class ProjectAuthorRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        class  ProjectAuthorRequirementHandler : IAuthorizationHandler<ProjectAuthorRequirement>
        {
            private readonly IApplicationDbContext _context;

            public  ProjectAuthorRequirementHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<AuthorizationResult> Handle(ProjectAuthorRequirement requirement, CancellationToken cancellationToken)
            {
                var userId = requirement.UserId;
                var projectId = requirement.ProjectId;
                
                var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == projectId && p.CreatedBy == userId);

                if (project != null)
                {
                    return AuthorizationResult.Succeed();
                }

                return AuthorizationResult.Fail("Action not allowed");
            }
        }
    }
}
