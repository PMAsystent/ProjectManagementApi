using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask;
using Task = Domain.Entities.Task;

namespace ProjectManagementApi.Controllers
{
    public class TasksController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Task>> AddTask([FromBody] CreateTaskCommand createTaskCommand)
        {
            var result = await Mediator.Send(createTaskCommand); 
            return Ok(result.Task);
        }
    }
}
