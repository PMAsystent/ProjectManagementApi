using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask;
using ProjectManagement.Core.UseCases.Tasks.Commands.DeleteTask;
using ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask;
using ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById;

namespace ProjectManagementApi.Controllers
{
    public class TasksController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTaskWithDetails(int id)
        {
            try
            {
                var getTaskByIdQuery = new GetTaskWithDetailsQuery()
                {
                    TaskId = id
                };
                var result = await Mediator.Send(getTaskByIdQuery);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] CreateTaskCommand createTaskCommand)
        {
            try
            {
                var result = await Mediator.Send(createTaskCommand);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleteTaskCommand = new DeleteTaskCommand()
                {
                    TaskId = id
                };
                await Mediator.Send(deleteTaskCommand);

                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateTaskCommand updateTaskCommand)
        {
            try
            {
                await Mediator.Send(updateTaskCommand);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}