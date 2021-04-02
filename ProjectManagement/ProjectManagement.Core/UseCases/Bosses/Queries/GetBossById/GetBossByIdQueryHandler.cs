using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Bosses.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Bosses.Queries.GetBossById
{
    public class GetBossByIdQueryHandler : IRequestHandler<GetBossByIdQuery, DetailedBossDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBossByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DetailedBossDto> Handle(GetBossByIdQuery request, CancellationToken cancellationToken)
        {
            var boss = await _context.Bosses.FindAsync(request.BossId);
            if (boss == null)
            {
                throw new NotFoundException(nameof(Boss), request.BossId);
            }

            var bossToReturn = _mapper.Map<DetailedBossDto>(boss);
            return bossToReturn;
        }
    }
}
