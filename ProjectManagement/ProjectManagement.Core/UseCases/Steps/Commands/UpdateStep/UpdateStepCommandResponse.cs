using Domain.Entities;
using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommandResponse : BaseResponse
    {
        public Step Step { get; set; }
        
        public UpdateStepCommandResponse() : base() {}
        public UpdateStepCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public UpdateStepCommandResponse(string message) : base(message){}
        public UpdateStepCommandResponse(string message, bool success) : base(message, success) {}
        public UpdateStepCommandResponse(Step step) => Step = step;
    }
}