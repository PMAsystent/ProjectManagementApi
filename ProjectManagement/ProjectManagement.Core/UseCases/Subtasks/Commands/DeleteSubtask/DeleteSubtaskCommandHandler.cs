using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.DeleteSubtask
{
    public class DeleteSubtaskCommandHandler : IRequestHandler<DeleteSubtaskCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSubtaskCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(DeleteSubtaskCommand request, CancellationToken cancellationToken)
        {
            var subtask = await _context.Subtasks.FindAsync(request.SubtaskId);
            if (subtask == null)
            {
                throw new NotFoundException(nameof(Subtask), request.SubtaskId);
            }

            _context.Subtasks.Remove(subtask);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}