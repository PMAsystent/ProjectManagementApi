using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

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
            await _context.Subtasks.AddAsync(subtask, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new(_mapper.Map<SubtaskDto>(subtask));
        }
    }
}