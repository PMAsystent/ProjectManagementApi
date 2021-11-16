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
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.Utils;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
using TaskStatus = Domain.Enums.TaskStatus;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetMyProjectsList
{
    public class GetMyProjectsQueryHandler : IRequestHandler<GetMyProjectsQuery, MyProjectsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProjectsPercentageService _projectsPercentageService;

        public GetMyProjectsQueryHandler(IApplicationDbContext context, IMapper mapper,
            IProjectsPercentageService projectsPercentageService)
        {
            _context = context;
            _mapper = mapper;
            _projectsPercentageService = projectsPercentageService;
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

            var stepsInMyProjects = await _context.Steps
                .Where(s => myProjects.Select(p => p.Id).Contains(s.ProjectId))
                .ToListAsync(cancellationToken);

            foreach (var project in myProjects)
            {
                var projectSteps = stepsInMyProjects.Where(s => s.ProjectId == project.Id).ToList();
                project.ProgressPercentage = _projectsPercentageService.GetProgressPercentageForProject(projectSteps);
            }

            return new()
            {
                ProjectsList = myProjects,
                Count = myProjects.Count
            };
        }
    }
}