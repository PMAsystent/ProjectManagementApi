using MediatR;
using ProjectManagement.Core.UseCases.Customers.ViewModels;

namespace ProjectManagement.Core.UseCases.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<CustomerVm>
    {
    }
}
