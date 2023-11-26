using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Commands.BrandAddCommand
{
    public class BrandAddRequest : IRequest<Brand>
    {
        public string Name { get; set; }
    }
}
