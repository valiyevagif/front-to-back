using MediatR;

namespace Bigon.Business.Modules.ColorsModule.Commands.ColorRemoveCommand
{
    public class ColorRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
