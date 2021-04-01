using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Bosses.Dto;
using ProjectManagement.Core.UseCases.Bosses.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.GetBosses
{
    public class GetBossesQueryHandler : IRequestHandler<GetBossesQuery, BossVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBossesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BossVm> Handle(GetBossesQuery request, CancellationToken cancellationToken)
        {
            var bosses =
                await _context.Bosses
                    .ProjectTo<BossDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return new()
            {
                Bosses = bosses,
                Count = bosses.Count
            };
        }
    }
}
