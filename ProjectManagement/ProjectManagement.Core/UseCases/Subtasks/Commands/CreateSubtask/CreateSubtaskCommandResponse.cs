using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask
{
    public class CreateSubtaskCommandResponse : BaseResponse
    {
        public SubtaskDto Subtask { get; set; }
        
        public CreateSubtaskCommandResponse() : base() {}
        public CreateSubtaskCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public CreateSubtaskCommandResponse(string message) : base(message){}
        public CreateSubtaskCommandResponse(string message, bool success) : base(message, success) {}
        public CreateSubtaskCommandResponse(SubtaskDto subtask) => Subtask = subtask;
    }
}