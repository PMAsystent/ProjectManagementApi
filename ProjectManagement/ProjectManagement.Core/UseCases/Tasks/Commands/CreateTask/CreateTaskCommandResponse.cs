using Domain.Entities;
using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandResponse : BaseResponse
    {
        public Task Task { get; set; }
        
        public CreateTaskCommandResponse() : base() {}
        public CreateTaskCommandResponse(ValidationResult validationResult) : base(validationResult) {}
        public CreateTaskCommandResponse(string message) : base(message){}
        public CreateTaskCommandResponse(string message, bool success) : base(message, success) {}
        public CreateTaskCommandResponse(Task task) => Task = task;
    }
}
