using System.Threading;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Authorization
{
    public class AssignedToProjectRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        class AssignedToProjectRequirementHandler : IAuthorizationHandler<AssignedToProjectRequirement>
        {
            private readonly IApplicationDbContext _context;

            public AssignedToProjectRequirementHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<AuthorizationResult> Handle(AssignedToProjectRequirement requirement, CancellationToken cancellationToken)
            {
                var userId = requirement.UserId;
                var assignment = await _context.ProjectAssignments
                    .FirstOrDefaultAsync(a => a.User.ApplicationUserId == userId && a.ProjectId == requirement.ProjectId, cancellationToken);

                if (assignment != null)
                {
                    return AuthorizationResult.Succeed();
                }

                return AuthorizationResult.Fail("Can not access project");
            }
        }
    }
}
