using MediatR;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public int TaskId { get; set; }
    }
}
