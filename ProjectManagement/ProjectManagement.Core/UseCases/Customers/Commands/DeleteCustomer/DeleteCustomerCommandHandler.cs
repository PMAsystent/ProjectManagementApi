using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
