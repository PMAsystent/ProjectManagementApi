using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Customers.Dto;
using ProjectManagement.Core.UseCases.Customers.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomerVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerVm> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers =
                await _context.Customers
                    .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return new()
            {
                Customers = customers,
                Count = customers.Count
            };
        }
    }
}
