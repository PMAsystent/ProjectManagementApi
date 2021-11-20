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
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, DetailedProjectDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetailedProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);
            project.IsActive = true;

            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var projectAssignments =
                await GetProjectAssignments(request.CurrentUserId, project.Id, request.AssignedUsers);
            await _context.ProjectAssignments.AddRangeAsync(projectAssignments, cancellationToken);
            await _context.Steps.AddAsync(new()
            {
                Name = "First step",
                ProjectId = project.Id
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return await GetDetailedProjectDto(project, projectAssignments);
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

        private async Task<List<ProjectAssignedUserDto>> GetProjectAssignedUsers(
            IEnumerable<ProjectAssignment> projectAssignments)
        {
            var assignedUsers =
                await _context.Users
                    .Where(u => projectAssignments
                        .Select(a => a.UserId)
                        .Contains(u.Id))
                    .ToListAsync();

            return (
                from projectAssignment in projectAssignments
                let user = assignedUsers.SingleOrDefault(u => u.Id == projectAssignment.UserId)
                where user != null
                select new ProjectAssignedUserDto()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    MemberType = projectAssignment.MemberType,
                    ProjectRole = projectAssignment.ProjectRole
                }).ToList();
        }

        private async Task<DetailedProjectDto> GetDetailedProjectDto(Project project,
            List<ProjectAssignment> projectAssignments)
        {
            var projectToReturn = _mapper.Map<DetailedProjectDto>(project);
            projectToReturn.ProjectTasks = new List<ProjectTaskDto>();
            projectToReturn.ProjectAssignedUsers = new List<ProjectAssignedUserDto>();

            var assignedUsers = await GetProjectAssignedUsers(projectAssignments);

            foreach (var assignment in projectAssignments)
            {
                projectToReturn.ProjectAssignedUsers.Add(new()
                {
                    UserId = assignment.UserId,
                    UserName = assignedUsers.SingleOrDefault(u => u.UserId == assignment.UserId)?.UserName,
                    MemberType = assignment.MemberType
                });
            }

            return projectToReturn;
        }
    }
}