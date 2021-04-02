using MediatR;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.DeleteBoss
{
    public class DeleteBossCommand : IRequest
    {
        public int BossId { get; set; }
    }
}
