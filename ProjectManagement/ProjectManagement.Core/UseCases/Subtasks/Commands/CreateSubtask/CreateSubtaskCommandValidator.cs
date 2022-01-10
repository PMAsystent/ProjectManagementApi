using FluentValidation;
using ProjectManagement.Core.Concrete.Identity.Commands;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask
{
    public class CreateSubtaskCommandValidator : AbstractValidator<CreateSubtaskCommand>
    {
        public CreateSubtaskCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.TaskId).GreaterThan(0);
        }
    }
}