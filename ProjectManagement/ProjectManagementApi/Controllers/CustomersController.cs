using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Customers.Commands.CreateCustomer;
using ProjectManagement.Core.UseCases.Customers.Commands.DeleteCustomer;
using ProjectManagement.Core.UseCases.Customers.Commands.UpdateCustomer;
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

        [HttpPost]
        public async Task<ActionResult<DetailedCustomerDto>> AddCustomer([FromBody] CreateCustomerCommand createPostCommand)
        {
            var result = await Mediator.Send(createPostCommand);
            return Ok(result.DetailedCustomerDto);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateCustomer([FromBody] UpdateCustomerCommand updateProjectCommand)
        {
            var result = await Mediator.Send(updateProjectCommand);
            return Ok(result.CustomerId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteCustomerCommand = new DeleteCustomerCommand()
            {
                CustomerId = id
            };
            await Mediator.Send(deleteCustomerCommand);

            return NoContent();
        }
    }
}
