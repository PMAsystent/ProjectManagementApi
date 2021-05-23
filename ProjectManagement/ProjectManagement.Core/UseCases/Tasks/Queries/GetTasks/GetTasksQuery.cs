using MediatR;
using ProjectManagement.Core.UseCases.Tasks.ViewModels;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTasks
{
    public class GetTasksQuery : IRequest<TaskVM> { }
}
