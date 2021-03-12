using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Commands.CreateProject;
using ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects;
using System;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    public class ProjectsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ProjectVm>> GetAllProjects()
        {
            return await Mediator.Send(new GetProjectsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProject([FromBody] CreateProjectCommand createPostCommand)
        {
            var result = await Mediator.Send(createPostCommand);
            return Ok(result.ProjectId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteProjectCommand = new DeleteProjectCommand()
            {
                ProjectId = id
            };
            var test = await Mediator.Send(deleteProjectCommand);
            Console.WriteLine();
            // TODO: check if project exist and send 404 when Not Found => create response with response status
            return NoContent();
        }
    }
}
