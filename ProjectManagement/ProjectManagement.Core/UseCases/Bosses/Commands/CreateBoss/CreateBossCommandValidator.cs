using FluentValidation;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.CreateBoss
{
    public class CreateBossCommandValidator : AbstractValidator<CreateBossCommand>
    {
        public CreateBossCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.Surname).NotEmpty();

            RuleFor(c => c.Email).EmailAddress();

            RuleFor(c => c.PhoneNumber).NotEmpty();
        }
    }
}
