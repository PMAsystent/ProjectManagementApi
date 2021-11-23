using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Utils;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectWithDetails
{
    public class GetProjectWithDetailsQueryHandler : IRequestHandler<GetProjectWithDetailsQuery, DetailedProjectDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProgressPercentageService _projectsPercentageService;

        public GetProjectWithDetailsQueryHandler(IApplicationDbContext context, IMapper mapper,
            IProgressPercentageService projectsPercentageService)
        {
            _context = context;
            _mapper = mapper;
            _projectsPercentageService = projectsPercentageService;
        }

        public async Task<DetailedProjectDto> Handle(GetProjectWithDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var detailedProjectDto = _mapper.Map<DetailedProjectDto>(project);

            var stepsInProject =
                await _context.Steps
                    .Include(s => s.Tasks)
                    .Where(s => s.ProjectId == detailedProjectDto.Id).ToListAsync(cancellationToken);

            detailedProjectDto.ProjectSteps = stepsInProject.Select(step => new ProjectStepDto()
            {
                Id = step.Id,
                Name = step.Name,
                ProgressPercentage = step.Tasks != null ? _projectsPercentageService.GetProgressPercentageForStep(step.Tasks.ToList()) : 0 
            }).ToList();

            detailedProjectDto.ProjectTasks =
                await _context.Tasks.Where(t => detailedProjectDto.ProjectSteps
                        .Select(s => s.Id)
                        .Contains(t.StepId))
                    .ProjectTo<ProjectTaskDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            detailedProjectDto.ProjectAssignedUsers = await GetProjectAssignedUsers(detailedProjectDto.Id);

            detailedProjectDto.ProgressPercentage =
                _projectsPercentageService.GetProgressPercentageForProject(stepsInProject);

            detailedProjectDto.CurrentUserInfoInProject =
                await GetCurrentUserInfoInProject(request.CurrentUserId, detailedProjectDto.ProjectAssignedUsers);


            return detailedProjectDto;
        }

        private async Task<ICollection<ProjectAssignedUserDto>> GetProjectAssignedUsers(int projectId)
        {
            var projectAssignments =
                await _context.ProjectAssignments
                    .Where(a => a.ProjectId == projectId)
                    .ToListAsync();

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

        private async Task<CurrentUserInfoInProject> GetCurrentUserInfoInProject(string userId,
            ICollection<ProjectAssignedUserDto> assignments)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.ApplicationUserId == userId);
            var currentUserAssignment = assignments.SingleOrDefault(a => a.UserId == currentUser.Id);
            if (currentUserAssignment == null)
            {
                throw new Exception("User isn't assigned to this project.");
            }

            return new()
            {
                ProjectRole = currentUserAssignment.ProjectRole,
                MemberType = currentUserAssignment.MemberType
            };
        }
    }
}