using MediatR;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.DeleteSubtask
{
    public class DeleteSubtaskCommand : IRequest
    {
        public int SubtaskId { get; set; }
    }
}