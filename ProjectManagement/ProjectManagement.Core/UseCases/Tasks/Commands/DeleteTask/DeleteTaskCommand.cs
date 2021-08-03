using MediatR;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public int TaskId { get; set; }
    }
}
