using System;
using FluentValidation;
using ProjectManagement.Core.UseCases.Steps.Commands.CreateStep;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommandValidator : AbstractValidator<UpdateStepCommand>
    {
        public UpdateStepCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Name).MaximumLength(30);
            RuleFor(s => s.Description).NotEmpty();
            RuleFor(s => s.Description).MaximumLength(100);
            //TODO: Add more rules
        }
        
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}