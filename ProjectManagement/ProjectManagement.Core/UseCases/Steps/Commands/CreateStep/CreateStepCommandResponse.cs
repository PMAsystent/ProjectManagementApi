using Domain.Entities;
using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;

namespace ProjectManagement.Core.UseCases.Steps.Commands.CreateStep
{
    public class CreateStepCommandResponse : BaseResponse
    {
        public Step Step { get; set; }
        
        public CreateStepCommandResponse() : base() {}
        public CreateStepCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public CreateStepCommandResponse(string message) : base(message){}
        public CreateStepCommandResponse(string message, bool success) : base(message, success) {}
        public CreateStepCommandResponse(Step step) => Step = step;
    }
}
