using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskName
{
    public class UpdateSubtaskNameCommandResponse : BaseResponse
    {
        public SubtaskDto Subtask { get; set; }
        
        public UpdateSubtaskNameCommandResponse() : base() {}
        public UpdateSubtaskNameCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public UpdateSubtaskNameCommandResponse(string message) : base(message){}
        public UpdateSubtaskNameCommandResponse(string message, bool success) : base(message, success) {}
        public UpdateSubtaskNameCommandResponse(SubtaskDto subtask) => Subtask = subtask;
    }
}