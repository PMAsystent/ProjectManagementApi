using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Bosses.Commands.CreateBoss;
using ProjectManagement.Core.UseCases.Bosses.Commands.DeleteBoss;
using ProjectManagement.Core.UseCases.Bosses.Commands.UpdateBoss;
using ProjectManagement.Core.UseCases.Bosses.Dto;
using ProjectManagement.Core.UseCases.Bosses.Queries.GetBossById;
using ProjectManagement.Core.UseCases.Bosses.Queries.GetBosses;
using ProjectManagement.Core.UseCases.Bosses.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagementApi.Controllers
{
    public class BossesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BossVm>> GetAllBosses()
        {
            return await Mediator.Send(new GetBossesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedBossDto>> GetBossById(int id)
        {
            var getBossQuery = new GetBossByIdQuery()
            {
                BossId = id
            };

            return await Mediator.Send(getBossQuery);
        }

        [HttpPost]
        public async Task<ActionResult<DetailedBossDto>> AddBoss([FromBody] CreateBossCommand createBossCommand)
        {
            var result = await Mediator.Send(createBossCommand);
            return Ok(result.DetailedBossDto);
        }

        [HttpPut]
        public async Task<ActionResult<DetailedBossDto>> UpdateBoss([FromBody] UpdateBossCommand updateBossCommand)
        {
            var result = await Mediator.Send(updateBossCommand);
            return Ok(result.DetailedBossDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoss(int id)
        {
            var deleteBossCommand = new DeleteBossCommand()
            {
                BossId = id
            };
            await Mediator.Send(deleteBossCommand);

            return NoContent();
        }
    }
}
