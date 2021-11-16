using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
using TaskStatus = Domain.Enums.TaskStatus;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetMyProjectsList
{
    public class GetMyProjectsQueryHandler : IRequestHandler<GetMyProjectsQuery, MyProjectsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMyProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MyProjectsListVm> Handle(GetMyProjectsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                .SingleOrDefaultAsync(u => u.ApplicationUserId == request.CurrentUserGuid, cancellationToken);

            var myProjectsIds = await _context.ProjectAssignments
                .Where(a => a.UserId == currentUser.Id)
                .Select(a => a.ProjectId)
                .ToListAsync(cancellationToken);

            var myProjects = await _context.Projects
                .Where(p => myProjectsIds.Contains(p.Id))
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);

            foreach (var project in myProjects)
            {
                project.ProgressPercentage = await GetProgressPercentageForProject(project.Id);
            }

            return new()
            {
                ProjectsList = myProjects,
                Count = myProjects.Count
            };
        }

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

            return allTasksInProject.Count(t => t.TaskStatus == TaskStatus.Done.ToString()) * 100 /
                   allTasksInProject.Count;
        }
    }
}