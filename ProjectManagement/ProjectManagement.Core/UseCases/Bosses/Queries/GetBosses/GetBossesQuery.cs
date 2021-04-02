using MediatR;
using ProjectManagement.Core.UseCases.Bosses.ViewModels;

namespace ProjectManagement.Core.UseCases.Bosses.Queries.GetBosses
{
    public class GetBossesQuery : IRequest<BossVm>
    {
    }
}
