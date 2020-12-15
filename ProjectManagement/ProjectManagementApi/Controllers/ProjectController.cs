using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
