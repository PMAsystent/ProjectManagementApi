using Domain.Entities;
using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Steps.Dto;

namespace ProjectManagement.Core.UseCases.Steps.Commands.CreateStep
{
    public class CreateStepCommandResponse : BaseResponse
    {
        public StepDto Step { get; set; }
        
        public CreateStepCommandResponse() : base() {}
        public CreateStepCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public CreateStepCommandResponse(string message) : base(message){}
        public CreateStepCommandResponse(string message, bool success) : base(message, success) {}
        public CreateStepCommandResponse(StepDto step) => Step = step;
    }
}
