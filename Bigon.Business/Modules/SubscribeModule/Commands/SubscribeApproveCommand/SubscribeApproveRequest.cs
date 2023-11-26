using MediatR;

namespace Bigon.Business.Modules.SubscribeModule.Commands.SubscribeApproveCommand
{
    public class SubscribeApproveRequest : IRequest
    {
        public string Token { get; set; }
    }
}
