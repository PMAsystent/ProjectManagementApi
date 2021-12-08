using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Infrastructure.Persistance.DatabaseContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Helpers
{
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ApplicationDbContext _context;

        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, ApplicationDbContext context) : base(options,
            logger, encoder, clock)
        {
            _context = context;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == "integration@tests.pl");
            var claims = new List<Claim>();

            if (user != null)
            {
                claims.Add(new(ClaimTypes.NameIdentifier, user.ApplicationUserId));
            }

            var identity = new ClaimsIdentity(claims, "Integration tests");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Integration tests");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }
}