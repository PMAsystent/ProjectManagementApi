using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Customers.Dto;

namespace ProjectManagement.Core.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandResponse : BaseResponse
    {
        public DetailedCustomerDto DetailedCustomerDto { get; set; }

        public CreateCustomerCommandResponse() : base() { }
        public CreateCustomerCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public CreateCustomerCommandResponse(string message) : base(message) { }
        public CreateCustomerCommandResponse(string message, bool success) : base(message, success) { }
        public CreateCustomerCommandResponse(DetailedCustomerDto detailedCustomerDto) => DetailedCustomerDto = detailedCustomerDto;
    }
}
