using System;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask;
using ProjectManagement.Core.UseCases.TaskAssignments.Commands.UnassignUserFromTask;
using ProjectManagement.Core.UseCases.TaskAssignments.Queries.GetUnassignedToTaskUsers;

namespace ProjectManagementApi.Controllers
{
    public class TaskAssignmentController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AssignUserToTask([FromBody] AssignUserToTaskCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (UnauthorizedException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete]
        public async Task<ActionResult> UnassignUserFromTask([FromBody] UnassignUserFromTaskCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (UnauthorizedException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("unassignedUsers/{taskId}")]
        public async Task<ActionResult> GetUnassignedUsers(int taskId)
        {
            try
            {
                var query = new GetUnassignedToTaskUserQuery
                {
                    TaskId = taskId
                };
                
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (UnauthorizedException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
