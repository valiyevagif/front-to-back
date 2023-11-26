using MediatR;

namespace Bigon.Business.Modules.SubscribeModule.Commands.SubscribeTicketCommand
{
    public class SubscribeTicketRequest : IRequest
    {
        public string Email { get; set; }
    }
}
