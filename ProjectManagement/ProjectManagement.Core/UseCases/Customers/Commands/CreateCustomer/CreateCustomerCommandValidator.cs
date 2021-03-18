using FluentValidation;

namespace ProjectManagement.Core.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(20);

            RuleFor(c => c.Surname).NotEmpty();
            RuleFor(c => c.Name).MaximumLength(30);

            RuleFor(c => c.Address).NotEmpty();

            RuleFor(c => c.Country).NotEmpty();

            RuleFor(c => c.PostCode).NotEmpty();

            //Phone number validation
            //RuleFor(c => c.PhoneNumber)
            RuleFor(c => c.Email).EmailAddress();
        }
    }
}
