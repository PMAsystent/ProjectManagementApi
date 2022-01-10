using System;
using FluentValidation;

namespace ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep
{
    public class UpdateStepCommandValidator : AbstractValidator<UpdateStepCommand>
    {
        public UpdateStepCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}