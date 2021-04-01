using FluentValidation;

namespace ProjectManagement.Core.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
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
