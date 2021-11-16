using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectById
{
    public class GetProjectWithDetailsQueryHandler : IRequestHandler<GetProjectWithDetailsQuery, DetailedProjectDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectWithDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            projectToReturn.ProgressPercentage = await GetProgressPercentageForProject(projectToReturn.Id);

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
                        .Select(a => a.Id)
                        .Contains(u.Id))
                    .ToListAsync();

            return (
                from projectAssignment in projectAssignments
                let user = assignedUsers.SingleOrDefault(u => u.Id == projectAssignment.UserId)
                where user != null
                select new ProjectAssignedUserDto()
                {
                    UserId = user.Id,
                    UserFirstName = user.FirstName,
                    UserLastName = user.LastName,
                    MemberType = projectAssignment.MemberType
                }).ToList();
        }

        // TODO: Duplicated code
        private async Task<int> GetProgressPercentageForProject(int projectId)
        {
            var stepsInProject = await _context.Steps.Where(s => s.ProjectId == projectId).ToListAsync();

            //TODO: What if 0 steps/tasks?
            if (stepsInProject.Count == 0)
            {
                return 0;
            }

            var allTasksInProject = new List<Domain.Entities.Task>();
            foreach (var step in stepsInProject)
            {
                allTasksInProject.AddRange(step.Tasks);
            }

            return allTasksInProject.Count(t => t.TaskStatus == Domain.Enums.TaskStatus.Done.ToString()) * 100 /
                   allTasksInProject.Count;
        }
    }
}