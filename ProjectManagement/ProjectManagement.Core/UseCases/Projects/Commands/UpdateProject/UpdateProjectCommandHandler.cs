using AutoMapper;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
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

            if (validatorResult.IsValid)
            {
                return new UpdateProjectCommandResponse(validatorResult);
            }

            var existingProject = await _context.Projects.FindAsync(request);
            var updatedProject = _mapper.Map(request, existingProject);
            await _context.SaveChangesAsync(cancellationToken);

            return new(updatedProject.Id);
        }
    }
}
