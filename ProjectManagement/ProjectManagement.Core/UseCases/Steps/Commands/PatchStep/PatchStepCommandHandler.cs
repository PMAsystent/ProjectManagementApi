using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Commands.PatchStep
{
    public class PatchStepCommandHandler : IRequestHandler<PatchStepCommand, PatchStepCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatchStepCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PatchStepCommandResponse> Handle(PatchStepCommand request, CancellationToken cancellationToken)
        {
            var step = await _context.Steps.FindAsync(request.StepId);

            if (step == null)
            {
                throw new NotFoundException(nameof(Step), request.StepId);
            }

            var stepDto = _mapper.Map<StepDto>(step);
            request.PatchDocument.ApplyTo(stepDto, request.ModelStateDictionary);

            if (!request.ModelStateDictionary.IsValid)
            {
                var test = request.ModelStateDictionary != null
                    ? new SerializableError(request.ModelStateDictionary)
                    : throw new ArgumentNullException(nameof(request.ModelStateDictionary));
                throw new Exception($"{test}");
            }

            _mapper.Map(stepDto, step);
            await _context.SaveChangesAsync(cancellationToken);

            return new(stepDto);
        }
    }
}