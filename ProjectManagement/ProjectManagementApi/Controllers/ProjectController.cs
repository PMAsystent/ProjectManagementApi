using Microsoft.AspNetCore.Mvc;
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
    }
}
