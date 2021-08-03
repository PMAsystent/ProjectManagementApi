using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Commands.PatchProject
{
    public class PatchProjectCommandHandler : IRequestHandler<PatchProjectCommand, PatchProjectCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatchProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PatchProjectCommandResponse> Handle(
            PatchProjectCommand request,
            CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var projectDto = _mapper.Map<ProjectDto>(project);
            request.PatchDocument.ApplyTo(projectDto, request.ModelStateDictionary);

            if (!request.ModelStateDictionary.IsValid)
            {
                var test =
                    request.ModelStateDictionary != null
                        ? new SerializableError(request.ModelStateDictionary)
                        : throw new ArgumentNullException(nameof(request.ModelStateDictionary));
                throw new Exception($"{test}");

            }

            _mapper.Map(projectDto, project);
            await _context.SaveChangesAsync(cancellationToken);

            var detailedProjectDto = _mapper.Map<DetailedProjectDto>(project);

            return new(detailedProjectDto);
        }
    }
}
