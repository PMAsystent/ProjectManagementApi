using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateProjectCommandResponse(validatorResult);
            }

            var existingProject = await _context.Projects.FindAsync(request.Id);
            if (existingProject == null)
            {
                throw new NotFoundException(nameof(Project), request.Id);

            }
            
            var updatedProject = _mapper.Map(request, existingProject);
            await _context.SaveChangesAsync(cancellationToken);

            var detailedProjectDto = _mapper.Map<DetailedProjectDto>(updatedProject);

            return new(detailedProjectDto);
        }
    }
}
