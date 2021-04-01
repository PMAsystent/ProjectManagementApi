using MediatR;
using ProjectManagement.Core.UseCases.Bosses.ViewModels;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.GetBosses
{
    public class GetBossesQuery : IRequest<BossVm>
    {
    }
}
