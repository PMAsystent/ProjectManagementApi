using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.UseCases.Projects.Dto;
using Task = System.Threading.Tasks.Task;

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

            var projectAssignments =
                await GetProjectAssignments(request.CurrentUserId, project.Id, request.AssignedUsersIds);
            await _context.ProjectAssignments.AddRangeAsync(projectAssignments, cancellationToken);
            await _context.Steps.AddAsync(new()
            {
                Name = "First step",
                ProjectId = project.Id
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<List<ProjectAssignment>> GetProjectAssignments(string currentUserId, int projectId,
            IEnumerable<CreateProjectAssignmentsDto> assignedUsers)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.ApplicationUserId == currentUserId);

            var projectAssignments = new List<ProjectAssignment>
            {
                new()
                {
                    UserId = currentUser.Id,
                    ProjectId = projectId,
                    MemberType = ProjectMemberType.Manager.ToString(),
                    ProjectRole = ProjectRole.SuperMember.ToString()
                }
            };

            projectAssignments.AddRange(assignedUsers.Select(
                assignedUser => new ProjectAssignment()
                {
                    UserId = assignedUser.UserId,
                    ProjectId = projectId,
                    MemberType = assignedUser.MemberType,
                    ProjectRole = assignedUser.ProjectRole,
                }));

            return projectAssignments;
        }
    }
}