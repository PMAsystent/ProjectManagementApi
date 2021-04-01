using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Bosses.Commands.GetBossById;
using ProjectManagement.Core.UseCases.Bosses.Commands.GetBosses;
using ProjectManagement.Core.UseCases.Bosses.Dto;
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
    }
}
