using FluentValidation;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
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
