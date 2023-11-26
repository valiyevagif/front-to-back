using MediatR;

namespace Bigon.Business.Modules.AccountModule.Commands.RefreshTokenCommand
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
        public string Token { get; set; }
    }
}
