using System;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Commands.CreateProject;
using ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
using System.Threading.Tasks;
using MediatR.Behaviors.Authorization.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Requests;
using ProjectManagement.Core.UseCases.Projects.Commands.ArchiveOrUnArchiveProject;
using ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject;
using ProjectManagement.Core.UseCases.Projects.Queries.GetMyProjectsList;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjectWithDetails;

namespace ProjectManagementApi.Controllers
{
    public class MyProjectsController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public MyProjectsController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<MyProjectsListVm>> GetMyProjects()
        {
            try
            {
                var query = new GetMyProjectsQuery(_currentUserService.UserId);
                var result = await Mediator.Send(query);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedProjectDto>> GetProjectWithDetails(int id)
        {
            try
            {
                var getProjectByIdQuery = new GetProjectWithDetailsQuery()
                {
                    ProjectId = id,
                    CurrentUserId = _currentUserService.UserId
                };
                var result = await Mediator.Send(getProjectByIdQuery);
                return Ok(result);
            }
            catch (UnauthorizedException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddProject([FromBody] CreateProjectRequest createProjectRequest)
        {
            try
            {
                var command = new CreateProjectCommand()
                {
                    Name = createProjectRequest.Name,
                    DueDate = createProjectRequest.DueDate,
                    Description = createProjectRequest.Description,
                    AssignedUsers = createProjectRequest.AssignedUsers,
                    CurrentUserId = _currentUserService.UserId
                };

                var result = await Mediator.Send(command);
                return Ok(result);
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
                var deleteProjectCommand = new DeleteProjectCommand()
                {
                    ProjectId = id
                };
                await Mediator.Send(deleteProjectCommand);

                return NoContent();
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
        public async Task<ActionResult> UpdateProject([FromBody] UpdateProjectCommand updateProjectCommand)
        {
            try
            {
                await Mediator.Send(updateProjectCommand);
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

        [HttpPatch("{id}/archive/{isActive}")]
        public async Task<ActionResult> ArchiveOrUnArchiveProject(int id, bool isActive)
        {
            try
            {
                var command = new ArchiveOrUnArchiveProjectCommand()
                {
                    IsActive = isActive,
                    ProjectId = id
                };

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