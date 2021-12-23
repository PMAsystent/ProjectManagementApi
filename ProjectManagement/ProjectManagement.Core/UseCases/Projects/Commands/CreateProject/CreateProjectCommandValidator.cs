using System;
using FluentValidation;

namespace ProjectManagement.Core.UseCases.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.DueDate).GreaterThanOrEqualTo(DateTime.UtcNow);
        }
    }
}