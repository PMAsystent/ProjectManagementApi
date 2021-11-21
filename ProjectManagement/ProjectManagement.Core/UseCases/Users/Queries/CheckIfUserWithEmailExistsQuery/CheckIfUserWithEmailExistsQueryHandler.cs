using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;

namespace ProjectManagement.Core.UseCases.Users.Queries.CheckIfUserWithEmailExistsQuery
{
    public class CheckIfUserWithEmailExistsQueryHandler : IRequestHandler<CheckIfUserWithEmailExistsQuery, bool>
    {
        private readonly IIdentityService _identityService;

        public CheckIfUserWithEmailExistsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(CheckIfUserWithEmailExistsQuery request, CancellationToken cancellationToken) =>
            await _identityService.CheckIfUserWithEmailExists(request.Email);
    }
}