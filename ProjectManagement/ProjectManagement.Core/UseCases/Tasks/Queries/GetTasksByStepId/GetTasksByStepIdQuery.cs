using MediatR;
using ProjectManagement.Core.UseCases.Tasks.ViewModels;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTasksByStepId
{
    public class GetTasksByStepIdQuery : IRequest<TaskVM>
    {
        public int StepId { get; set; }
    }
}
