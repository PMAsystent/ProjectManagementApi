using System;
using FluentValidation;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name).MaximumLength(30);
            RuleFor(t => t.Description).NotEmpty();
            RuleFor(t => t.Description).MaximumLength(100);
            //TODO: Add more rules
        }
        
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
