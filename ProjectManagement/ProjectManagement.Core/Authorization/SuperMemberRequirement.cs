using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Authorization
{
    public class SuperMemberRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public int? ProjectId { get; set; }
        public int? StepId { get; set; }
        public int? TaskId { get; set; }

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
                
                if (projectId == null)
                {
                    var step = new Step();
                    
                    if (requirement.StepId != null)
                    {
                        step = await _context.Steps.FindAsync(requirement.StepId);
                    }
                    else if (requirement.TaskId != null)
                    {
                        step = await _context.Steps.FirstOrDefaultAsync(s =>
                            s.Tasks.FirstOrDefault(t => t.Id == requirement.TaskId) != null, cancellationToken);
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                    if (step == null)
                    {
                        return AuthorizationResult.Fail("Action not allowed");
                    }
                    
                    projectId = step.ProjectId;
                }
                
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
