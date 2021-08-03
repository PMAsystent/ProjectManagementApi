using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommandResponse : BaseResponse
    {
        public DetailedProjectDto DetailedProjectDto { get; set; }

        public CreateProjectCommandResponse() : base() { }
        public CreateProjectCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public CreateProjectCommandResponse(string message) : base(message) { }
        public CreateProjectCommandResponse(string message, bool success) : base(message, success) { }
        public CreateProjectCommandResponse(DetailedProjectDto detailedProjectDto) => DetailedProjectDto = detailedProjectDto;
    }
}
