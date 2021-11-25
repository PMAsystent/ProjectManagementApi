using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Concrete.ProjectAssigment.Commands
{
    public class DeleteUsersAssignmentsCommand : IRequest
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }

    public class DeleteUsersAssignmentsCommandHandler : IRequestHandler<DeleteUsersAssignmentsCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUsersAssignmentsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUsersAssignmentsCommand request, CancellationToken cancellationToken)
        {
            var assignments = await _context.TaskAssignments.Include(a => a.Task)
                .Include(a => a.Task.Step)
                .Where(a => a.Task.Step.ProjectId == request.ProjectId && a.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            assignments.ForEach(a =>
                {
                    a.isActive = false;
                });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
