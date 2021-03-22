using FluentValidation;

namespace ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Id must be more than zero");

            // TODO: Think about solution with avoiding code repeating (create project command validator & this)

            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.Description).NotEmpty();

            RuleFor(c => c.StartDate)
                .LessThan(c => c.EndDate)
                .WithMessage("Start date must be earlier than end date.")
                .LessThan(c => c.TargetDate)
                .WithMessage("Start date must be earlier than target date.");

            RuleFor(c => c.CustomerId)
                .GreaterThan(0)
                .WithMessage("Customer id must be more than zero");
        }
    }
}
