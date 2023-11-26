using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ColorsModule.Commands.ColorEditCommand
{
    public class ColorEditRequest : IRequest<Color>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
