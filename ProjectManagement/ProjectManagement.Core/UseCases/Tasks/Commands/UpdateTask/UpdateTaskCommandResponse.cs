using Domain.Entities;
using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandResponse : BaseResponse
    {
        public Task Task { get; set; }
        
        public UpdateTaskCommandResponse() : base() {}
        public UpdateTaskCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public UpdateTaskCommandResponse(string message) : base(message){}
        public UpdateTaskCommandResponse(string message, bool success) : base(message, success) {}
        public UpdateTaskCommandResponse(Task task) => Task = task;
    }
}
