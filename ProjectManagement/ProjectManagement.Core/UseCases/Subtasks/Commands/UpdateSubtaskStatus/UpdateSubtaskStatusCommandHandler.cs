using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskStatus
{
    public class UpdateSubtaskStatusCommandHandler : IRequestHandler<UpdateSubtaskStatusCommand, UpdateSubtaskStatusCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSubtaskStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<UpdateSubtaskStatusCommandResponse> Handle(UpdateSubtaskStatusCommand request, CancellationToken cancellationToken)
        {
            var existingSubtask = await _context.Subtasks.FindAsync(request.SubtaskId);
            if (existingSubtask == null)
            {
                throw new NotFoundException(nameof(Subtask), request.SubtaskId);
            }

            existingSubtask.IsDone = request.IsDone;
            await _context.SaveChangesAsync(cancellationToken);

            return new(_mapper.Map<SubtaskDto>(existingSubtask));
        }
    }
}