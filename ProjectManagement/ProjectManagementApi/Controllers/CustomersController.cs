using Microsoft.AspNetCore.Mvc;
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
    }
}
