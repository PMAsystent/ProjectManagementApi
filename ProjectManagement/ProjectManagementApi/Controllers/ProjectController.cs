using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Commands.CreateProject;
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

        [HttpPost]
        public async Task<ActionResult<int>> AddProject([FromBody] CreatePostCommand createPostCommand)
        {
            var result = await Mediator.Send(createPostCommand);
            return Ok(result.ProjectId);
        }
    }
}
