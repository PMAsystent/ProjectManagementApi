using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Steps.Commands.CreateStep;
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

        [HttpPost]
        public async Task<ActionResult<Step>> AddStep([FromBody] CreateStepCommand createStepCommand)
        {
            var result = await Mediator.Send(createStepCommand);
            return Ok(result.Step);
        }
    }
}
