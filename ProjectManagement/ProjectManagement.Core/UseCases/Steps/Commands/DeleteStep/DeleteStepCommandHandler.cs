using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Steps.Commands.DeleteStep
{
    public class DeleteStepCommandHandler : IRequestHandler<DeleteStepCommand>
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

            if (request.MoveTasks)
            {
                var steps = await _context.Steps
                    .Where(s => s.ProjectId == step.ProjectId)
                    .OrderBy(s => s.Id)
                    .ToListAsync(cancellationToken);

                if (steps?.Count > 2)
                {
                    steps.First(s => s.Id != request.StepId).Tasks.ToList().AddRange(step.Tasks);
                }
                else
                {
                    throw new Exception("No more steps in the project");
                }
            }
            
            
            
            _context.Steps.Remove(step);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}