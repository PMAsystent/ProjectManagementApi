using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Customers.Dto;
using ProjectManagement.Core.UseCases.Customers.Queries.GetCustomerById;
using ProjectManagement.Core.UseCases.Customers.Queries.GetCustomers;
using ProjectManagement.Core.UseCases.Customers.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    public class CustomersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerVm>> GetAllCustomers()
        {
            return await Mediator.Send(new GetCustomersQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedCustomerDto>> GetCustomerById(int id)
        {
            var getCustomerQuery = new GetCustomerByIdQuery()
            {
                CustomerId = id
            };
            return await Mediator.Send(getCustomerQuery);
        }
    }
}
