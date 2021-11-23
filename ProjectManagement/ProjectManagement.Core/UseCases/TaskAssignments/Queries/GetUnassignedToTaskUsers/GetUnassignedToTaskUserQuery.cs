using MediatR;
using ProjectManagement.Core.UseCases.TaskAssignments.ViewModels;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Queries.GetUnassignedToTaskUsers
{
    public class GetUnassignedToTaskUserQuery : IRequest<UnassignedUsersVm>
    {
        public int TaskId { get; set; }
    }
}
