using MediatR;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskStatus
{
    public class UpdateSubtaskStatusCommand : IRequest<UpdateSubtaskStatusCommandResponse>
    {
        public int SubtaskId { get; set; }
        public bool IsDone { get; set; }
    }
}