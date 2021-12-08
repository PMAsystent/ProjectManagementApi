using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskStatus
{
    public class UpdateSubtaskStatusCommandResponse : BaseResponse
    {
        public SubtaskDto Subtask { get; set; }
        
        public UpdateSubtaskStatusCommandResponse() : base() {}
        public UpdateSubtaskStatusCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public UpdateSubtaskStatusCommandResponse(string message) : base(message){}
        public UpdateSubtaskStatusCommandResponse(string message, bool success) : base(message, success) {}
        public UpdateSubtaskStatusCommandResponse(SubtaskDto subtask) => Subtask = subtask;
    }
}