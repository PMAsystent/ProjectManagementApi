using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Commands.UnassignUserFromTask
{
    public class UnassignUserFromTaskCommandHandler : IRequestHandler<UnassignUserFromTaskCommand>
    {
        private readonly IApplicationDbContext _context;

        public UnassignUserFromTaskCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(UnassignUserFromTaskCommand request, CancellationToken cancellationToken)
        {
            var taskAssignment = await _context.TaskAssignments.FirstOrDefaultAsync(ta => 
                ta.TaskId == request.TaskId && ta.UserId == request.UserId && ta.isActive, cancellationToken);
            
            if (taskAssignment == null)
            {
                throw new NotFoundException(
                    $"{nameof(TaskAssignments)} with user({request.UserId} in project({request.TaskId}) does not exists.");
            }

            taskAssignment.isActive = false;
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
