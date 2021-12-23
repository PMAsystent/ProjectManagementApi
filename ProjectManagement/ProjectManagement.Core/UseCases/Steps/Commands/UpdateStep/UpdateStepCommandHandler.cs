using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommandHandler : IRequestHandler< UpdateStepCommand, UpdateStepCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateStepCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateStepCommandResponse> Handle(UpdateStepCommand request, CancellationToken cancellationToken)
        {
            var existingStep = await _context.Steps.FindAsync(request.Id);
            if (existingStep == null)
            {
                throw new NotFoundException(nameof(Step), request.Id);
            }
            
            var updatedStep = _mapper.Map(request, existingStep);
            await _context.SaveChangesAsync(cancellationToken);

            return new(_mapper.Map<StepDto>(updatedStep));

        }
    }
}
