using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Mappings;

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
            var validator = new UpdateStepCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateStepCommandResponse(validatorResult);
            }

            var existingStep = await _context.Steps.FindAsync(request.Id);
            if (existingStep == null)
            {
                throw new NotFoundException(nameof(Step), request.Id);
            }

            var updatedStep = _mapper.Map(request, existingStep);
            await _context.SaveChangesAsync(cancellationToken);

            return new(updatedStep);

        }
    }
}
