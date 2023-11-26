using MediatR;
using System.Security.Claims;

namespace Bigon.Business.Modules.AccountModule.Commands.BindClaimsCommand
{
    public class BindClaimsRequest : IRequest
    {
        public ClaimsIdentity Identity { get; set; }
    }
}
