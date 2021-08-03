using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Commands.PatchStep
{
    public class PatchStepCommandResponse : BaseResponse
    {
        public StepDto StepDto { get; set; }
        
        public PatchStepCommandResponse() : base() { }
        public PatchStepCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public PatchStepCommandResponse(string message) : base(message) { }
        public PatchStepCommandResponse(string message, bool success) : base(message, success) { }
        public PatchStepCommandResponse(StepDto stepDto) => StepDto = stepDto;
    }
}