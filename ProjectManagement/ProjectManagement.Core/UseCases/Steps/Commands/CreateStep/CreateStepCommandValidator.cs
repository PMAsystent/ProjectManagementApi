using System;
using FluentValidation;

namespace ProjectManagement.Core.UseCases.Steps.Commands.CreateStep
{
    public class CreateStepCommandValidator : AbstractValidator<CreateStepCommand>
    {
        public CreateStepCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}