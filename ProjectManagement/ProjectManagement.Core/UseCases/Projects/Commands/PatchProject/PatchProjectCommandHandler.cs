using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Commands.PatchProject
{
    public class PatchProjectCommandHandler : IRequestHandler<PatchProjectCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatchProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(
            PatchProjectCommand request,
            CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var projectDto = _mapper.Map<ProjectDto>(project);
            // TODO: zabezpieczenie przed nullem
            // ten model state by tu siadł... 
            request.PatchDocument.ApplyTo(projectDto);

            _mapper.Map(projectDto, project);
            await _context.SaveChangesAsync(cancellationToken);

            return project.Id;
        }
    }
}
