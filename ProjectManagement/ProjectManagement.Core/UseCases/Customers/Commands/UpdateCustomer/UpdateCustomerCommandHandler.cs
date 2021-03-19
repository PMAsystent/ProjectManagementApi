using AutoMapper;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateCustomerCommandResponse(validatorResult);
            }

            var existingCustomer = await _context.Customers.FindAsync(request.Id);
            if (existingCustomer == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);

            }

            var updatedCustomer = _mapper.Map(request, existingCustomer);
            await _context.SaveChangesAsync(cancellationToken);

            return new(updatedCustomer.Id);
        }
    }
}
