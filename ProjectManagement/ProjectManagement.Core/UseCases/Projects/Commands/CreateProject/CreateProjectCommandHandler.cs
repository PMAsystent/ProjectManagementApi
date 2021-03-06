using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreatePostCommandHandler : IRequestHandler<CreateProjectCommand, CreatePostCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreatePostCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePostCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreatePostCommandResponse(validatorResult);
            }

            var project = _mapper.Map<Project>(request);
            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Console.WriteLine(project.Id);
            return new(project.Id);
        }
    }
}
