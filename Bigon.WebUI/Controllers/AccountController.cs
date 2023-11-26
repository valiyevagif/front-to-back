using AngleSharp.Io;
using Bigon.Business.Modules.AccountModule.Commands.EmailConfirmCommand;
using Bigon.Business.Modules.AccountModule.Commands.RegisterCommand;
using Bigon.Business.Modules.AccountModule.Commands.SigninCommand;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bigon.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [Route("/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/signin.html")]
        public async Task<IActionResult> Signin(SigninRequest request)
        {
            var user = await mediator.Send(request);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                });

            var callback = Request.Query["ReturnUrl"];

            if (!string.IsNullOrWhiteSpace(callback))
            {
                return Redirect(callback);
            }

            return RedirectToAction("index", "home");
        }

        [AllowAnonymous]
        [Route("/register.html")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/register.html")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Signin));
        }

        [AllowAnonymous]
        [Route("/email-confirm.html")]
        public async Task<IActionResult> EmailConfirm(EmailConfirmRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Signin));
        }

        [Route("/accessdenied.html")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/logout.html")]
        public async Task<IActionResult> Logout()
        {
            await Request.HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
