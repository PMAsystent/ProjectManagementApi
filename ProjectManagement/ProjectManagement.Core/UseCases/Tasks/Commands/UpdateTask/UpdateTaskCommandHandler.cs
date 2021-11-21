using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using Task = Domain.Entities.Task;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        
        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existingTask = await _context.Tasks.FindAsync(request.Id);
            if (existingTask == null)
            {
                throw new NotFoundException(nameof(Task), request.Id);
            }

            var updatedTask = _mapper.Map(request, existingTask);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
