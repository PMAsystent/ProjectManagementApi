using System;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Subtasks.Commands.CreateSubtask;
using ProjectManagement.Core.UseCases.Subtasks.Commands.DeleteSubtask;
using ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskName;
using ProjectManagement.Core.UseCases.Subtasks.Commands.UpdateSubtaskStatus;
using ProjectManagement.Core.UseCases.Subtasks.Dto;

namespace ProjectManagementApi.Controllers
{
    public class SubtasksController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<SubtaskDto>> AddSubtask([FromBody] CreateSubtaskCommand createSubtaskCommand)
        {
            try
            {
                var result = await Mediator.Send(createSubtaskCommand);
                return Ok(result.Subtask);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleteSubtaskCommand = new DeleteSubtaskCommand
                {
                    SubtaskId = id
                };

                await Mediator.Send(deleteSubtaskCommand);

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

        [HttpPut("updateStatus/{id}")]
        public async Task<ActionResult<SubtaskDto>> UpdateSubtaskStatus([FromRoute] int id, [FromBody] bool isDone)
        {
            try
            {
                var updateSubtaskStatusCommand = new UpdateSubtaskStatusCommand
                {
                    SubtaskId = id,
                    IsDone = isDone
                };

                var result = await Mediator.Send(updateSubtaskStatusCommand);
                
                return Ok(result.Subtask);
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
        
        [HttpPut("updateName/{id}")]
        public async Task<ActionResult<SubtaskDto>> UpdateSubtaskName([FromRoute] int id, [FromBody] string name)
        {
            try
            {
                var updateSubtaskNameCommand = new UpdateSubtaskNameCommand
                {
                    SubtaskId = id,
                    Name = name
                };

                var result = await Mediator.Send(updateSubtaskNameCommand);
                
                return Ok(result.Subtask);
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