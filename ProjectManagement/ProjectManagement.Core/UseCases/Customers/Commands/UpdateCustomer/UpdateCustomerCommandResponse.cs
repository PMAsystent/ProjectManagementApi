using FluentValidation.Results;
using ProjectManagement.Core.Base.Responses;
using ProjectManagement.Core.UseCases.Customers.Dto;

namespace ProjectManagement.Core.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandResponse : BaseResponse
    {
        public DetailedCustomerDto DetailedCustomerDto { get; set; }

        public UpdateCustomerCommandResponse() : base() { }
        public UpdateCustomerCommandResponse(ValidationResult validationResult) : base(validationResult) { }
        public UpdateCustomerCommandResponse(string message) : base(message) { }
        public UpdateCustomerCommandResponse(string message, bool success) : base(message, success) { }
        public UpdateCustomerCommandResponse(DetailedCustomerDto detailedCustomerDto) => DetailedCustomerDto = detailedCustomerDto;
    }
}
