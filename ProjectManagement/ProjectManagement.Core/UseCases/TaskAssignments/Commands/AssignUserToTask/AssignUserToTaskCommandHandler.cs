using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask
{
    public class AssignUserToTaskCommandHandler : IRequestHandler<AssignUserToTaskCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AssignUserToTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(AssignUserToTaskCommand request, CancellationToken cancellationToken)
        {
            var taskAssigment = _mapper.Map<TaskAssignment>(request);
            taskAssigment.isActive = true;
            await _context.TaskAssignments.AddAsync(taskAssigment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
