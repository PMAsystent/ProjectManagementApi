using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Bosses.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Bosses.Commands.UpdateBoss
{
    public class UpdateBossCommandHandler : IRequestHandler<UpdateBossCommand, UpdateBossCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBossCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UpdateBossCommandResponse> Handle(UpdateBossCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBossCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new(validatorResult);
            }

            var existingBoss = await _context.Bosses.FindAsync(request.Id);
            if (existingBoss == null)
            {
                throw new NotFoundException(nameof(Boss), request.Id);
            }

            var updatedBoss = _mapper.Map(request, existingBoss);
            await _context.SaveChangesAsync(cancellationToken);

            var detailedBossDto = _mapper.Map<DetailedBossDto>(updatedBoss);

            return new(detailedBossDto);
        }
    }
}
