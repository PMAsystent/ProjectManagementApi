using MediatR;

namespace ProjectManagement.Core.UseCases.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
    }
}
