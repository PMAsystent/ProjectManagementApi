using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.Id);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.Id);

            }
            
            _mapper.Map(request, project);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
