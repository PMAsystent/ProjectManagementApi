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
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.Utils;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectWithDetails
{
    public class GetProjectWithDetailsQueryHandler : IRequestHandler<GetProjectWithDetailsQuery, DetailedProjectDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProjectsPercentageService _projectsPercentageService;

        public GetProjectWithDetailsQueryHandler(IApplicationDbContext context, IMapper mapper,
            IProjectsPercentageService projectsPercentageService)
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

            var projectToReturn = _mapper.Map<DetailedProjectDto>(project);

            projectToReturn.ProjectTasks =
                await _context.Tasks.Where(t => projectToReturn.ProjectSteps
                        .Select(s => s.Id)
                        .Contains(t.StepId))
                    .ProjectTo<ProjectTaskDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            projectToReturn.ProjectAssignedUsers = await GetProjectAssignedUsers(projectToReturn.Id);

            var stepsInProject =
                await _context.Steps.Where(s => s.ProjectId == projectToReturn.Id).ToListAsync(cancellationToken);

            projectToReturn.ProjectSteps = stepsInProject.Select(step => new ProjectStepDto()
            {
                Id = step.Id,
                Name = step.Name,
                ProgressPercentage = _projectsPercentageService.GetProgressPercentageForProject(new List<Step> { step })
            }).ToList();

            projectToReturn.ProgressPercentage =
                _projectsPercentageService.GetProgressPercentageForProject(stepsInProject);

            return projectToReturn;
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
                    UserName = "", //TODO: Konrad podstaw tutaj user.UserName :*
                    MemberType = projectAssignment.MemberType
                }).ToList();
        }
    }
}