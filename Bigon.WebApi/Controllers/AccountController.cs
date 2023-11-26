using Bigon.Business.Modules.AccountModule.Commands.RefreshTokenCommand;
using Bigon.Business.Modules.AccountModule.Commands.SigninCommand;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Concrates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Bigon.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtService jwtService;
        private readonly IUserManager userManager;

        public AccountController(IMediator mediator, IJwtService jwtService, IUserManager userManager)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
            this.userManager = userManager;
        }

        //https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "bu metod ile access ve refresh token alinir",Summary = "login metodu")]
        public async Task<IActionResult> Signin(SigninRequest request)
        {
            var user = await mediator.Send(request);

            var token = jwtService.GenerateAccessToken(user);
            var refreshToken = await userManager.GenerateRefreshTokenAsync(user, token);

            return Ok(new
            {
                access_token = token,
                refresh_token = refreshToken
            });
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "bu metod ile access ve refresh token gonderib yeni tokenleri almaq mumkundur", Summary = "refresh token metodu")]
        public async Task<IActionResult> RefreshToken([FromHeader] RefreshTokenRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(new
            {
                access_token = response.AccessToken,
                refresh_token = response.RefreshToken
            });
        }
    }
}
