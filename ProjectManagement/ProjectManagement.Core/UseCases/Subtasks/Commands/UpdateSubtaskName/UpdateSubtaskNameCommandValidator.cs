using FluentValidation;

namespace ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskName
{
    public class UpdateSubtaskNameCommandValidator : AbstractValidator<UpdateSubtaskNameCommand>
    {
        public UpdateSubtaskNameCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}