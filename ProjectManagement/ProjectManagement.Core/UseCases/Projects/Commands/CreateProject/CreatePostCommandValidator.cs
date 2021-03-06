using FluentValidation;
using System;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(100);
            //RuleFor(c => c.StartDate)
            //    .Must(BeAValidDate).WithMessage("Start date is required");
            //RuleFor(c => c.TargetDate)
            //    .Must(BeAValidDate).WithMessage("Target date is required");
            //RuleFor(c => c.EndDate)
            //    .Must(BeAValidDate).WithMessage("End date is required");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
