using System;
using FluentValidation;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommandValidator : AbstractValidator<UpdateStepCommand>
    {
        public UpdateStepCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Name).MaximumLength(30);
            //TODO: Add more rules
        }
        
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}