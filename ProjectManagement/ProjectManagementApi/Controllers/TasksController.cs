using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask;
using ProjectManagement.Core.UseCases.Tasks.Commands.DeleteTask;
using ProjectManagement.Core.UseCases.Tasks.Commands.PatchTask;
using ProjectManagement.Core.UseCases.Tasks.Commands.UpdateTask;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById;
using ProjectManagement.Core.UseCases.Tasks.Queries.GetTasks;
using ProjectManagement.Core.UseCases.Tasks.Queries.GetTasksByStepId;
using ProjectManagement.Core.UseCases.Tasks.ViewModels;
using Task = Domain.Entities.Task;

namespace ProjectManagementApi.Controllers
{
    public class TasksController : ApiControllerBase
    {
        // [HttpPost]
        // public async Task<ActionResult<Task>> AddTask([FromBody] CreateTaskCommand createTaskCommand)
        // {
        //     var result = await Mediator.Send(createTaskCommand); 
        //     return Ok(result.Task);
        // }
        //
        // [HttpGet]
        // public async Task<ActionResult<TaskVM>> GetAllTasks()
        // {
        //     return await Mediator.Send(new GetTasksQuery());
        // }
        //
        //
        // [HttpGet("{id}")]
        // public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        // {
        //     var getTaskByIdQuery = new GetTaskByIdQuery()
        //     {
        //         TaskId = id
        //     };
        //
        //     return await Mediator.Send(getTaskByIdQuery);
        // }
        //
        // [HttpGet("Step/{stepId}")]
        // public async Task<ActionResult<TaskVM>> GetTasksByStepId(int stepId)
        // {
        //     var getTaskByStepIdQuery = new GetTasksByStepIdQuery()
        //     {
        //         StepId = stepId
        //     };
        //
        //     return await Mediator.Send(getTaskByStepIdQuery);
        // }
        //
        // [HttpPatch(Name = "PatchTask")]
        // public async Task<ActionResult> PatchTask(int id, [FromBody] JsonPatchDocument<TaskDto> patchDocument)
        // {
        //     var patchTaskCommand = new PatchTaskCommand()
        //     {
        //         TaskId = id,
        //         PatchDocument = patchDocument,
        //         ModelStateDictionary = ModelState
        //     };
        //
        //     var result = await Mediator.Send(patchTaskCommand);
        //     return Ok(result);
        // }
        //
        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     var deleteTaskCommand = new DeleteTaskCommand()
        //     {
        //         TaskId = id
        //     };
        //     await Mediator.Send(deleteTaskCommand);
        //
        //     return NoContent();
        // }
        //
        // [HttpPut(Name = "Update Task")]
        // public async Task<ActionResult> Update([FromBody] UpdateTaskCommand updateTaskCommand)
        // {
        //     var result = await Mediator.Send(updateTaskCommand);
        //     return Ok(result.Task);
        // }
        //
    }
}
