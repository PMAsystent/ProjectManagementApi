using FluentValidation;

namespace ProjectManagement.Core.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Id must be more than zero");

            // TODO: Think about solution with avoiding code repeating (create project command validator & this)

            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.Surname).NotEmpty();

            RuleFor(c => c.Email).EmailAddress();

            RuleFor(c => c.PhoneNumber).NotEmpty();

            RuleFor(c => c.Address).NotEmpty();

            RuleFor(c => c.PostCode).NotEmpty();

            RuleFor(c => c.Country).NotEmpty();
        }
    }
}
