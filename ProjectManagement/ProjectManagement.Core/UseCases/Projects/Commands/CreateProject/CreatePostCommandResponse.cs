using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreatePostCommandResponse : BaseResponse
    {
        public int? ProjectId { get; set; }

        public CreatePostCommandResponse() : base() { }
        public CreatePostCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public CreatePostCommandResponse(string message) : base(message) { }
        public CreatePostCommandResponse(string message, bool success) : base(message, success) { }
        public CreatePostCommandResponse(int projectId) => ProjectId = projectId;
    }
}
