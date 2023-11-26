using MediatR;

namespace Bigon.Business.Modules.FaqsModule.Commands.FaqRemoveCommand
{
    public class FaqRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
