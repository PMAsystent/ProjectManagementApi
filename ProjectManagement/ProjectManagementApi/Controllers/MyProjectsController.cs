using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Commands.CreateProject;
using ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject;
using ProjectManagement.Core.UseCases.Projects.Commands.PatchProject;
using ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjectById;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
using System.Threading.Tasks;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Requests;
using ProjectManagement.Core.UseCases.Projects.Queries.GetMyProjectsList;

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
                var query = new GetMyProjectsQuery()
                {
                    CurrentUserGuid = _currentUserService.UserId
                };

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
                    ProjectId = id
                };
                var result = await Mediator.Send(getProjectByIdQuery);
                return Ok(result);
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
                //TODO: Add project in response?
                var command = new CreateProjectCommand()
                {
                    Name = createProjectRequest.Name,
                    Description = createProjectRequest.Description,
                    AssignedEmails = createProjectRequest.AssignedEmails,
                    CurrentUserId = _currentUserService.UserId
                };

                await Mediator.Send(command);
                return Ok();
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //
        // [HttpPut]
        // public async Task<ActionResult<DetailedProjectDto>> UpdateProject([FromBody] UpdateProjectCommand updateProjectCommand)
        // {
        //     var result = await Mediator.Send(updateProjectCommand);
        //     return Ok(result.DetailedProjectDto);
        // }
        //
        // [HttpPatch("{id}")]
        // public async Task<ActionResult> PatchProject(int id, [FromBody] JsonPatchDocument<ProjectDto> patchDocument)
        // {
        //     var patchProjectCommand = new PatchProjectCommand()
        //     {
        //         ProjectId = id,
        //         PatchDocument = patchDocument,
        //         ModelStateDictionary = ModelState
        //     };
        //     var result = await Mediator.Send(patchProjectCommand);
        //     return Ok(result);
        // }
        //
    }
}