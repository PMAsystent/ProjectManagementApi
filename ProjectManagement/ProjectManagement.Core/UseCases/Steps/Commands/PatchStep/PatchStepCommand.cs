using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Commands.PatchStep
{
    public class PatchStepCommand : IRequest<PatchStepCommandResponse>
    {
        public int StepId { get; set; }
        public JsonPatchDocument<StepDto> PatchDocument { get; set; }
        public ModelStateDictionary ModelStateDictionary { get; set; }
    }
}