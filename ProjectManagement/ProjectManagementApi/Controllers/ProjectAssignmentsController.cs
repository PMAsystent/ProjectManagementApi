using System;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.ProjectAssignments.Commands.AddUserToProject;
using ProjectManagement.Core.UseCases.ProjectAssignments.Commands.RemoveUserFromProject;
using ProjectManagement.Core.UseCases.ProjectAssignments.Commands.UpdateProjectAssignment;

namespace ProjectManagementApi.Controllers
{
    public class ProjectAssignmentsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddUserToProject([FromBody] AddUserToProjectCommand command)
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
        public async Task<ActionResult> RemoveUserFromProject([FromBody] RemoveUserFromProjectCommand command)
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

        [HttpPut]
        public async Task<ActionResult> UpdateProjectAssignment([FromBody] UpdateProjectAssignmentCommand command)
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
    }
}