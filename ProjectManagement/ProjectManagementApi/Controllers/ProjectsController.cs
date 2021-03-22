using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Commands.CreateProject;
using ProjectManagement.Core.UseCases.Projects.Commands.DeleteProject;
using ProjectManagement.Core.UseCases.Projects.Commands.PatchProject;
using ProjectManagement.Core.UseCases.Projects.Commands.UpdateProject;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjectByCustomer;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjectById;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
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

        [HttpGet("Customer/{customerId}")]
        public async Task<ActionResult<ProjectVm>> GetProjectsByCustomer(int customerId)
        {
            var getAllProjectsByCustomer = new GetProjectsByCustomerQuery()
            {
                CustomerId = customerId
            };
            return await Mediator.Send(getAllProjectsByCustomer);
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
        public async Task<ActionResult<ProjectDto>> AddProject([FromBody] CreateProjectCommand createPostCommand)
        {
            var result = await Mediator.Send(createPostCommand);
            return Ok(result.DetailedProjectDto);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateProject([FromBody] UpdateProjectCommand updateProjectCommand)
        {
            var result = await Mediator.Send(updateProjectCommand);
            return Ok(result.ProjectId);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchProject(int id, [FromBody] JsonPatchDocument<ProjectDto> patchDocument)
        {
            var patchProjectCommand = new PatchProjectCommand()
            {
                ProjectId = id,
                PatchDocument = patchDocument
            };
            var result = await Mediator.Send(patchProjectCommand);
            return Ok(result);
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
