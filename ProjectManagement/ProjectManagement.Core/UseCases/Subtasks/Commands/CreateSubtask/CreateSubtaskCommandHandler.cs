using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Subtasks.Dto;
using Task = Domain.Entities.Task;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask
{
    public class CreateSubtaskCommandHandler : IRequestHandler<CreateSubtaskCommand, CreateSubtaskCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSubtaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<CreateSubtaskCommandResponse> Handle(CreateSubtaskCommand request, CancellationToken cancellationToken)
        {
            var subtask = _mapper.Map<Subtask>(request);

            if (!await TaskExists(request.TaskId))
            {
                throw new NotFoundException(nameof(Task), request.TaskId);
            }
            
            await _context.Subtasks.AddAsync(subtask, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new(_mapper.Map<SubtaskDto>(subtask));
        }
        
        private async Task<bool> TaskExists(int taskId) => await _context.Tasks.FindAsync(taskId) != null;
    }
}