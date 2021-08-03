using MediatR;
using ProjectManagement.Core.UseCases.Steps.ViewModels;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetStepByProjectId
{
    public class GetStepsByProjectIdQuery : IRequest<StepVm>
    {
        public int ProjectId { get; set; }
    }
}