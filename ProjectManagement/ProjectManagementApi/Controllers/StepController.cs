using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Steps.Queries.GetSteps;

namespace ProjectManagementApi.Controllers
{
    public class StepController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<StepVm>> GetAllSteps()
        {
            return await Mediator.Send(new GetStepsQuery());
        }
    }
}
