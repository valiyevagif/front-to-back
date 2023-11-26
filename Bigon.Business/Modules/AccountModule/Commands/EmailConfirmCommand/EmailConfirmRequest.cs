using MediatR;

namespace Bigon.Business.Modules.AccountModule.Commands.EmailConfirmCommand
{
    public class EmailConfirmRequest : IRequest
    {
        public string Token { get; set; }
    }
}
