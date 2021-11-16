using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ChangeUserEmailCommand : IRequest
    {
        public string ApplicationUserEmail { get; set; }
        public string ApplicationUserNewEmail { get; set; }
    }

    public class ChangeUserEmailCommandHandler : IRequestHandler<ChangeUserEmailCommand>
    {
        private readonly IApplicationDbContext _context;

        public ChangeUserEmailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Single(u => u.Email == request.ApplicationUserEmail);
            user.Email = request.ApplicationUserNewEmail;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}