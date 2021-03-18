using MediatR;

namespace ProjectManagement.Core.UseCases.Steps.Commands.DeleteStep
{
    public class DeleteStepCommand : IRequest
    {
        public int StepId { get; set; }
    }
}