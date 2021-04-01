using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Projects.Dto;

namespace ProjectManagement.Core.UseCases.Projects.Commands.PatchProject
{
    public class PatchProjectCommandResponse : BaseResponse
    {
        public DetailedProjectDto DetailedProjectDto { get; set; }

        public PatchProjectCommandResponse() : base() { }
        public PatchProjectCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public PatchProjectCommandResponse(string message) : base(message) { }
        public PatchProjectCommandResponse(string message, bool success) : base(message, success) { }
        public PatchProjectCommandResponse(DetailedProjectDto detailedProjectDto) => DetailedProjectDto = detailedProjectDto;
    }
}
