using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.PatchTask
{
    public class PatchTaskCommandHandler : IRequestHandler<PatchTaskCommand, PatchTaskCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatchTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PatchTaskCommandResponse> Handle(PatchTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(request.TaskId);

            if (task == null)
            {
                throw new NotFoundException(nameof(Task), request.TaskId);
            }

            var taskDto = _mapper.Map<TaskDto>(task);
            request.PatchDocument.ApplyTo(taskDto, request.ModelStateDictionary);
            
            if (request.ModelStateDictionary.IsValid)
            {
                var test = request.ModelStateDictionary != null
                    ? new SerializableError(request.ModelStateDictionary)
                    : throw new ArgumentNullException(nameof(request.ModelStateDictionary));
                throw new Exception($"{test}");
            }

            _mapper.Map(taskDto, task);
            await _context.SaveChangesAsync(cancellationToken);
            
            return new (taskDto);
        }
    }
}
