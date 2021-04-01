using MediatR;
using ProjectManagement.Core.UseCases.Customers.Dto;

namespace ProjectManagement.Core.UseCases.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<DetailedCustomerDto>
    {
        public int CustomerId { get; set; }
    }
}
