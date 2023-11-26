using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Commands.BrandRemoveCommand
{
    public class BrandRemoveRequest : IRequest
    {
        public byte Id { get; set; }
    }
}
