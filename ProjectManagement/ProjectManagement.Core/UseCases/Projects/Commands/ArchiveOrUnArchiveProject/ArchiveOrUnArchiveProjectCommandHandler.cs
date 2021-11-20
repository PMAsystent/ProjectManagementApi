using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Projects.Commands.ArchiveOrUnArchiveProject
{
    public class ArchiveOrUnArchiveProjectCommandHandler : IRequestHandler<ArchiveOrUnArchiveProjectCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArchiveOrUnArchiveProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(ArchiveOrUnArchiveProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }
            
            _mapper.Map(request, project);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}