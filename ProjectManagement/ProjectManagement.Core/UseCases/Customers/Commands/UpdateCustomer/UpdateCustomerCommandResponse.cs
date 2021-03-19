using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;

namespace ProjectManagement.Core.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandResponse : BaseResponse
    {
        public int? CustomerId { get; set; }

        public UpdateCustomerCommandResponse() : base() { }
        public UpdateCustomerCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public UpdateCustomerCommandResponse(string message) : base(message) { }
        public UpdateCustomerCommandResponse(string message, bool success) : base(message, success) { }
        public UpdateCustomerCommandResponse(int customerId) => CustomerId = customerId;
    }
}
