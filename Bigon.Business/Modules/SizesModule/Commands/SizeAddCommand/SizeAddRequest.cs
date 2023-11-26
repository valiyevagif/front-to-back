using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.SizesModule.Commands.SizeAddCommand
{
    public class SizeAddRequest : IRequest<Size>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
