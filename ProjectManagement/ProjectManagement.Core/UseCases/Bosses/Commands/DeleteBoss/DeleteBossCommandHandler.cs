using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.DeleteBoss
{
    public class DeleteBossCommandHandler : IRequestHandler<DeleteBossCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBossCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteBossCommand request, CancellationToken cancellationToken)
        {
            var boss = await _context.Bosses.FindAsync(request.BossId);
            if (boss == null)
            {
                throw new NotFoundException(nameof(Boss), request.BossId);
            }

            _context.Bosses.Remove(boss);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
