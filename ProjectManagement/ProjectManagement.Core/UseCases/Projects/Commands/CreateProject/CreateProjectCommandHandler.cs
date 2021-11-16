using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);
            project.IsActive = true;

            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var currentUser = await _context.Users
                .SingleOrDefaultAsync(u => u.ApplicationUserId == request.CurrentUserId,
                    cancellationToken);
            
            // TODO: Move to project created event?
            var projectAssignments = new List<ProjectAssignment>
            {
                new()
                {
                    UserId = currentUser.Id,
                    ProjectId = project.Id,
                    MemberType = ProjectMemberType.Manager.ToString(),
                    ProjectRole = ProjectMemberType.Manager.ToString()
                }
            };

            // TODO: Move to project created event?
            foreach (var email in request.AssignedEmails)
            {
                //TODO: 
            }

            await _context.ProjectAssignments.AddRangeAsync(projectAssignments);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}