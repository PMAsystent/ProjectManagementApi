using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Commands.CreateStep
{
    public class CreateStepCommandHandler : IRequestHandler<CreateStepCommand, CreateStepCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateStepCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateStepCommandResponse> Handle(CreateStepCommand request, CancellationToken cancellationToken)
        {
            var step = _mapper.Map<Step>(request);
            await _context.Steps.AddAsync(step, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return new(_mapper.Map<StepDto>(step));
        }
    }
}
