using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.RemoveUserFromProject
{
    public class RemoveUserFromProjectCommandHandler : IRequestHandler<RemoveUserFromProjectCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveUserFromProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveUserFromProjectCommand request, CancellationToken cancellationToken)
        {
            var projectAssignment = await _context.ProjectAssignments.SingleOrDefaultAsync(a =>
                a.UserId == request.UserId && a.ProjectId == request.ProjectId, cancellationToken);
            if (projectAssignment == null)
            {
                throw new NotFoundException(
                    $"{nameof(ProjectAssignment)} with user({request.UserId} in project({request.ProjectId}) does not exists.");
            }

            _context.ProjectAssignments.Remove(projectAssignment);
            await _context.SaveChangesAsync(cancellationToken);
            
            //TODO: Remove assigned tasks
            
            return Unit.Value;
        }
    }
}