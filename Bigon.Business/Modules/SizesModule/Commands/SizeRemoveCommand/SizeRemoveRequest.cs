using MediatR;

namespace Bigon.Business.Modules.SizesModule.Commands.SizeRemoveCommand
{
    public class SizeRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
