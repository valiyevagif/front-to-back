using Bigon.Infrastructure.Entities.Membership;
using MediatR;
using System.Security.Claims;

namespace Bigon.Business.Modules.AccountModule.Commands.SigninCommand
{
    public class SigninRequest : IRequest<BigonUser>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
