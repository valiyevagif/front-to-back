using Bigon.Infrastructure.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace Bigon.Business.Modules.AccountModule.Commands.SigninCommand
{
    internal class SigninRequestHandler : IRequestHandler<SigninRequest, BigonUser>
    {
        private readonly UserManager<BigonUser> userManager;
        private readonly SignInManager<BigonUser> signInManager;
        private readonly IActionContextAccessor ctx;

        public SigninRequestHandler(UserManager<BigonUser> userManager, SignInManager<BigonUser> signInManager, IActionContextAccessor ctx)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.ctx = ctx;
        }

        public async Task<BigonUser> Handle(SigninRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserName);

            if (user == null)
                throw new Exception($"{request.UserName} user not found!");

            var checkResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (!checkResult.Succeeded)
                throw new Exception($"Username or Password is incorrect!");

            

            return user;
        }
    }
}
