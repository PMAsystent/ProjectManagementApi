using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskName
{
    public class UpdateSubtaskNameCommandHandler : IRequestHandler<UpdateSubtaskNameCommand, UpdateSubtaskNameCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSubtaskNameCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        } 
        
        public async Task<UpdateSubtaskNameCommandResponse> Handle(UpdateSubtaskNameCommand request, CancellationToken cancellationToken)
        {
            var existingSubtask = await _context.Subtasks.FindAsync(request.SubtaskId);
            if (existingSubtask == null)
            {
                throw new NotFoundException(nameof(Subtask), request.SubtaskId);
            }

            existingSubtask.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return new(_mapper.Map<SubtaskDto>(existingSubtask));
        }
    }
}