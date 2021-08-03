using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetStepById
{
    public class GetStepByIdQueryHandler : IRequestHandler<GetStepByIdQuery, StepDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStepByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<StepDto> Handle(GetStepByIdQuery request, CancellationToken cancellationToken)
        {
            var step = await _context.Steps.FindAsync(request.StepId);
            if (step == null)
            {
                throw new NotFoundException(nameof(Step), request.StepId);
            }

            var stepToReturn = _mapper.Map<StepDto>(step);

            return stepToReturn;
        }
    }
}