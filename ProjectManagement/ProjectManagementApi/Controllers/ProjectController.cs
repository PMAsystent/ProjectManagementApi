using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Commands.CreateProject;
using ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjectById;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjectById.Dto;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedProjectDto>> GetProjectById(int id)
        {
            var getProjectByIdQuery = new GetProjectByIdQuery()
            {
                ProjectId = id
            };

            return await Mediator.Send(getProjectByIdQuery);
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
            await Mediator.Send(deleteProjectCommand);

            return NoContent();
        }
    }
}
