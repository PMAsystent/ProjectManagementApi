using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using Task = Domain.Entities.Task;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            //TODO: Remove validation because we dont need it, probably.
            
            var validator = new CreateTaskCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new(validationResult);
            }

            var task = _mapper.Map<Task>(request);
            await _context.Tasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new(task);
            
        }
    }
}
