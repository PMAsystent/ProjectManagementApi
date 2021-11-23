using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Authorization
{
    public class AssignedToProjectRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public int? ProjectId { get; set; }
        public int? StepId { get; set; }
        public int? TaskId { get; set; }

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
                var projectId = requirement.ProjectId;
                
                if (projectId == null)
                {
                    var step = new Step();

                    if (requirement.StepId != null)
                    {
                        step = await _context.Steps.FindAsync(requirement.StepId);
                    }
                    else if (requirement.TaskId != null)
                    {
                        var task = await _context.Tasks.Include(t => t.Step)
                            .FirstOrDefaultAsync(t => t.Id == requirement.TaskId, cancellationToken);
                        step = task?.Step;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    if (step == null)
                    {
                        return AuthorizationResult.Fail("Can not access");
                    }

                    projectId = step.ProjectId;
                }

                var assignment = await _context.ProjectAssignments
                    .FirstOrDefaultAsync(a => a.User.ApplicationUserId == userId && a.ProjectId == projectId, cancellationToken);

                if (assignment != null)
                {
                    return AuthorizationResult.Succeed();
                }

                return AuthorizationResult.Fail("Can not access");
            }
        }
    }
}
