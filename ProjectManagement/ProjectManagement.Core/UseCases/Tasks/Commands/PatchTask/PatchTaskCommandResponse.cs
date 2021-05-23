using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.PatchTask
{
    public class PatchTaskCommandResponse : BaseResponse
    {
        public TaskDto TaskDto { get; set; }
        
        public PatchTaskCommandResponse() : base() { }
        public PatchTaskCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public PatchTaskCommandResponse(string message) : base(message) { }
        public PatchTaskCommandResponse(string message, bool success) : base(message, success) { }
        public PatchTaskCommandResponse(TaskDto taskDto) => TaskDto = taskDto;
        
    }
}
