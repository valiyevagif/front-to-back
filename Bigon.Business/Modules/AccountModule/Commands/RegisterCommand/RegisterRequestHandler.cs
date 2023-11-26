using Bigon.Infrastructure.Entities.Membership;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text;
using System.Web;

namespace Bigon.Business.Modules.AccountModule.Commands.RegisterCommand
{
    internal class RegisterRequestHandler : IRequestHandler<RegisterRequest>
    {
        private readonly UserManager<BigonUser> userManager;
        private readonly RoleManager<BigonRole> roleManager;
        private readonly IEmailService emailService;
        private readonly IActionContextAccessor ctx;
        private readonly ICryptoService cryptoService;

        public RegisterRequestHandler(UserManager<BigonUser> userManager,
            RoleManager<BigonRole> roleManager,
            IEmailService emailService,
            IActionContextAccessor ctx,
            ICryptoService cryptoService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.emailService = emailService;
            this.ctx = ctx;
            this.cryptoService = cryptoService;
        }
        public async Task Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                throw new Exception($"{request.Email} user already taken!");
            }


            user = new BigonUser
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                EmailConfirmed = false
            };

            int? tryCount = null;

            while (true)
            {
                user.UserName = $"{request.Name}.{request.Surname}{(tryCount.HasValue ? tryCount.ToString() : "")}".ToLower();
                if (await userManager.FindByNameAsync(user.UserName) == null)
                    break;

                tryCount = (tryCount ?? 0) + 1;
            }

            var userCreateResult = await userManager.CreateAsync(user, request.Password);

            if (!userCreateResult.Succeeded)
            {
                var sb = new StringBuilder();

                foreach (var item in userCreateResult.Errors)
                {
                    sb.AppendLine($"{item.Code}: {item.Description}");
                }

                throw new Exception(sb.ToString());
            }

            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            token = HttpUtility.UrlEncode(token);

            token = cryptoService.Encrypt($"t={token}&email={user.Email}", true);

            string url = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/email-confirm.html?token={token}";

            string message = $"Qeydiyyatiniz uğurla tamamlandır.Hesabınızı aktivləşdirmək üçün <a href='{url}'>link</a>`lə davam edin.";

            await emailService.SendMailAsync(request.Email, "Bigon Reqistration", message);
        }
    }
}
