using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.ProjectAssignments.Commands.RemoveUserFromProject
{
    public class RemoveUserFromProjectCommandHandler : IRequestHandler<RemoveUserFromProjectCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDomainEventService _domainEventService;

        public RemoveUserFromProjectCommandHandler(IApplicationDbContext context, IDomainEventService domainEventService)
        {
            _context = context;
            _domainEventService = domainEventService;
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
            
            await _domainEventService.Publish(new ProjectAssignmentDeletedEvent(request.UserId, request.ProjectId));

            return Unit.Value;
        }
    }
}