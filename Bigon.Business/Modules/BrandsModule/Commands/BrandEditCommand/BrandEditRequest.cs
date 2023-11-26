using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Commands.BrandEditCommand
{
    public class BrandEditRequest : IRequest<Brand>
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
