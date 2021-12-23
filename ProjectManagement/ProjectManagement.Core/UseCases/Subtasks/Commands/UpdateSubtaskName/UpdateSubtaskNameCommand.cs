using MediatR;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskName
{
    public class UpdateSubtaskNameCommand : IRequest<UpdateSubtaskNameCommandResponse>
    {
    public int SubtaskId { get; set; }
    public string Name { get; set; }
    }
}