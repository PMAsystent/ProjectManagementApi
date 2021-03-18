using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Steps.Commands.CreateStep;
using ProjectManagement.Core.UseCases.Steps.Commands.DeleteStep;
using ProjectManagement.Core.UseCases.Steps.Commands.UpdateStep;
using ProjectManagement.Core.UseCases.Steps.Queries.GetSteps;
using Task = Domain.Entities.Task;

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

        [HttpPut(Name = "UpdateStep")]
        public async Task<ActionResult> Update([FromBody] UpdateStepCommand updateStepCommand)
        {
            var result = await Mediator.Send(updateStepCommand);
            return Ok(result.Step);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteStepCommand = new DeleteStepCommand()
            {
                StepId = id
            };
            await Mediator.Send(deleteStepCommand);

            return NoContent();
        }
    }
}
