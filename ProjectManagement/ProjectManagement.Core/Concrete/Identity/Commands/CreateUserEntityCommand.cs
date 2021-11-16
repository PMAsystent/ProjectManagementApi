using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class CreateUserEntityCommand : IRequest
    {
        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationUserEmail { get; set; }
    }

    public class CreateUserEntityCommandHandler : IRequestHandler<CreateUserEntityCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserEntityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                ApplicationUserId = request.ApplicationUserId,
                UserName = request.ApplicationUserName,
                Email = request.ApplicationUserEmail
            };
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}