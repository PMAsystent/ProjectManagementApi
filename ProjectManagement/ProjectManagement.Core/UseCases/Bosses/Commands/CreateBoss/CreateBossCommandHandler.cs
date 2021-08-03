using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Bosses.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.CreateBoss
{
    public class CreateBossCommandHandler : IRequestHandler<CreateBossCommand, CreateBossCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateBossCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CreateBossCommandResponse> Handle(CreateBossCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBossCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateBossCommandResponse(validatorResult);
            }

            var boss = _mapper.Map<Boss>(request);

            await _context.Bosses.AddAsync(boss, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var bossToReturn = _mapper.Map<DetailedBossDto>(boss);

            return new(bossToReturn);
        }
    }
}
