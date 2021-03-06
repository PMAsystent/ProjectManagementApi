using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    public class ProjectController : ApiControllerBase
    {
        [HttpGet]
        [Route("")]
        [Route("ProjectManagement")]
        public async Task<ActionResult<ProjectVm>> GetFields()
        {
            return await Mediator.Send(new GetProjectsQuery());
        }
    }
}
