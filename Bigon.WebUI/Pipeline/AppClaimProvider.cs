using Bigon.Business.Modules.AccountModule.Commands.BindClaimsCommand;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Bigon.WebUI.Pipeline
{
    public class AppClaimProvider : IClaimsTransformation
    {
        private readonly IMediator mediator;

        public AppClaimProvider(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                await mediator.Send(new BindClaimsRequest { Identity = identity });
            }

            return principal;
        }
    }
}
