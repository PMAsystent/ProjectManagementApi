using MediatR;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetStepById
{
    public class GetStepByIdQuery : IRequest<StepDto>
    {
        public int StepId { get; set; }
    }
}