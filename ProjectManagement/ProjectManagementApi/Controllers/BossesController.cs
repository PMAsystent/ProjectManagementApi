using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Core.UseCases.Bosses.Commands.GetBosses;
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
    }
}
