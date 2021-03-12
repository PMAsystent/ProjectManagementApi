using MediatR;
using ProjectManagement.Core.UseCases.Projects.ViewModels;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<ProjectVm> { }
}
