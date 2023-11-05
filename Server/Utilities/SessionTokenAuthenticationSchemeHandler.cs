using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace VideoNestServer.Utilities
{
    public class SessionTokenAuthenticationSchemeHandler : AuthenticationHandler<SessionTokenAuthenticationSchemeOptions>
    {
        public readonly TokenManager _tokenManager;

        public SessionTokenAuthenticationSchemeHandler(
            IOptionsMonitor<SessionTokenAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            TokenManager tokenManager) : base(options, logger, encoder, clock)
        {
            _tokenManager = tokenManager;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Cookies.TryGetValue("token", out string? token))
            {
                return AuthenticateResult.Fail("User not authenticated");
            }

            if (token == null)
            {
                return AuthenticateResult.Fail("User not authenticated");
            }

            ClaimsPrincipal? principal = _tokenManager.VerifyToken(token);

            if (principal == null)
            {
                return AuthenticateResult.Fail("User not authenticated");
            }

            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
