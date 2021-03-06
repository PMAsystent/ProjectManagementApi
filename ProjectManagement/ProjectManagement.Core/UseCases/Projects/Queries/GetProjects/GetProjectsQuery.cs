using MediatR;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<ProjectVm> { }
}
