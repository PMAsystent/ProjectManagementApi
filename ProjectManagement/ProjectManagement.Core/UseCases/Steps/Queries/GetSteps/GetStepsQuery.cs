using MediatR;
using ProjectManagement.Core.UseCases.Steps.ViewModels;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetSteps
{
    public class GetStepsQuery : IRequest<StepVm> { }
}
