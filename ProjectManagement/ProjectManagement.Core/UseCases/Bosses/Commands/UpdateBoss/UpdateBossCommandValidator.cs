using FluentValidation;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.UpdateBoss
{
    public class UpdateBossCommandValidator : AbstractValidator<UpdateBossCommand>
    {
        public UpdateBossCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Id must be more than zero");

            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.Surname).NotEmpty();

            RuleFor(c => c.Email).EmailAddress();

            RuleFor(c => c.PhoneNumber).NotEmpty();
        }
    }
}
