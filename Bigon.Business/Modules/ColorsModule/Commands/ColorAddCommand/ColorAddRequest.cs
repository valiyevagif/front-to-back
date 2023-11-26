using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ColorsModule.Commands.ColorAddCommand
{
    public class ColorAddRequest : IRequest<Color>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
