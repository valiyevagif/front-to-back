using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery
{
    public class BrandGetByIdRequest : IRequest<Brand>
    {
        public int Id { get; set; }
    }
}
