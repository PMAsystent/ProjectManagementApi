using MediatR;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById
{
    public class GetTaskWithDetailsQuery : IRequest<DetailedTaskDto>
    {
        public int TaskId { get; set; }
    }
}
