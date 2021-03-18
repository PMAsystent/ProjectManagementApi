using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Steps.Commands.DeleteStep
{
    public class DeleteStepCommandHandler : IRequest<DeleteStepCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStepCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(DeleteStepCommand request, CancellationToken cancellationToken)
        {
            var step = await _context.Steps.FindAsync(request.StepId);
            if (step == null)
            {
                throw new NotFoundException(nameof(Step), request.StepId);
            }

            _context.Steps.Remove(step);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}