using FluentValidation;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(30);

            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(100);
        }
    }
}
