using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Authorization
{
    public class SuperMemberRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        class SuperMemberRequirementHandler : IAuthorizationHandler<SuperMemberRequirement>
        {
            private readonly IApplicationDbContext _context;

            public SuperMemberRequirementHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<AuthorizationResult> Handle(SuperMemberRequirement requirement, CancellationToken cancellationToken)
            {
                var userId = requirement.UserId;
                var projectId = requirement.ProjectId;
                
                var assignment = await _context.ProjectAssignments
                    .FirstOrDefaultAsync(a => 
                        a.ProjectId == projectId && a.User.ApplicationUserId == userId && a.ProjectRole == ProjectRole.SuperMember.ToString(),cancellationToken);

                if (assignment != null)
                {
                    return AuthorizationResult.Succeed();
                }

                return AuthorizationResult.Fail("Action not allowed");
            }
        }
    }
}
