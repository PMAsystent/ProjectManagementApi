using System.Threading;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Authorization
{
    public class AssignedToTaskRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public int TaskId { get; set; }

        class AssignedToTaskRequirementHandler : IAuthorizationHandler<AssignedToTaskRequirement>
        {
            private readonly IApplicationDbContext _context;

            public AssignedToTaskRequirementHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<AuthorizationResult> Handle(AssignedToTaskRequirement requirement, CancellationToken cancellationToken)
            {
                var userId = requirement.UserId;
                var taskId = requirement.TaskId;
                
                var assignment = await _context.TaskAssignments
                    .FirstOrDefaultAsync(a => a.Id == taskId && a.User.ApplicationUserId == userId, cancellationToken);

                if (assignment != null)
                {
                    return AuthorizationResult.Succeed();
                }

                return AuthorizationResult.Fail("Can not access task");
            }
        }
    }
}
