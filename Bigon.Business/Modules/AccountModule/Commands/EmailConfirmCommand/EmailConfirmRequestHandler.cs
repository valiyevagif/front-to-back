using Bigon.Infrastructure.Entities.Membership;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Bigon.Business.Modules.AccountModule.Commands.EmailConfirmCommand
{
    internal class EmailConfirmRequestHandler : IRequestHandler<EmailConfirmRequest>
    {
        private readonly UserManager<BigonUser> userManager;
        private readonly ICryptoService cryptoService;

        public EmailConfirmRequestHandler(UserManager<BigonUser> userManager,ICryptoService cryptoService)
        {
            this.userManager = userManager;
            this.cryptoService = cryptoService;
        }

        public async Task Handle(EmailConfirmRequest request, CancellationToken cancellationToken)
        {
            var token = cryptoService.Decrypt(request.Token);

            var tokenInfo = token.RegisterConfirmToken();

            var user = await userManager.FindByEmailAsync(tokenInfo.email);
            await userManager.ConfirmEmailAsync(user, tokenInfo.token);
        }
    }
}
