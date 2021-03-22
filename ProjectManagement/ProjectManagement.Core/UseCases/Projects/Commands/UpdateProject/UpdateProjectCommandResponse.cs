using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandResponse : BaseResponse
    {
        public DetailedProjectDto DetailedProjectDto { get; set; }

        public UpdateProjectCommandResponse() : base() { }
        public UpdateProjectCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public UpdateProjectCommandResponse(string message) : base(message) { }
        public UpdateProjectCommandResponse(string message, bool success) : base(message, success) { }
        public UpdateProjectCommandResponse(DetailedProjectDto detailedProjectDto) => DetailedProjectDto = detailedProjectDto;

    }
}
