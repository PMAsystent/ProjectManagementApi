using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;
using ProjectManagement.Core.UseCases.Steps.ViewModels;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetSteps
{
    class GetStepsQueryHandler : IRequestHandler<GetStepsQuery, StepVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStepsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<StepVm> Handle(GetStepsQuery request, CancellationToken cancellationToken)
        {
            var steps =  await _context.Steps
                .ProjectTo<StepDto>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToListAsync(cancellationToken);

            return new()
            {
                StepList = steps,
                Count = steps.Count
            };

        }
    }
}
