using Bigon.Infrastructure.Exceptions;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Concrates;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.AccountModule.Commands.RefreshTokenCommand
{
    internal class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly IJwtService jwtService;
        private readonly ICryptoService cryptoService;
        private readonly IIdentityService identityService;
        private readonly IUserManager userManager;
        private readonly IActionContextAccessor ctx;

        public RefreshTokenRequestHandler(IJwtService jwtService,
            ICryptoService cryptoService,
            IIdentityService identityService,
            IUserManager userManager,
            IActionContextAccessor ctx)
        {
            this.jwtService = jwtService;
            this.cryptoService = cryptoService;
            this.identityService = identityService;
            this.userManager = userManager;
            this.ctx = ctx;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetPrincipalId();

            if (!userId.HasValue)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    ["BAD_ACCESS_TOKEN"] = new[] { "BAD_ACCESS_TOKEN" }
                };

                throw new BadRequestException("BAD_DATA", errors);
            }

            request.Token = ctx.GetHeaderValue("refresh_token");

            if (string.IsNullOrWhiteSpace(request.Token))
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    ["BAD_REFRESH_TOKEN"] = new[] { "BAD_REFRESH_TOKEN" }
                };

                throw new BadRequestException("BAD_DATA", errors);
            }


            request.Token = cryptoService.ToMd5(request.Token);

            var user = await userManager.GetUserById(userId.Value, cancellationToken);

            if (!await userManager.ValidateRefreshTokenAsync(user, request.Token, cancellationToken))
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    ["BAD_REFRESH_TOKEN"] = new[] { "BAD_REFRESH_TOKEN" }
                };

                throw new BadRequestException("BAD_DATA", errors);
            }

            var response = new RefreshTokenResponse
            {
                AccessToken = jwtService.GenerateAccessToken(user)
            };

            response.RefreshToken = await userManager.GenerateRefreshTokenAsync(user, response.AccessToken, cancellationToken);

            return response;
        }
    }
}
