using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using ProjectManagement.Core.Base.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApi.Middleware
{
public class TokenManager : ITokenManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public TokenManager(
                IHttpContextAccessor httpContextAccessor, IIdentityService identityService, ICurrentUserService currentUserService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
        {
            return await _identityService.CheckLogoutAsync(_currentUserService.UserId);
        }

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}
