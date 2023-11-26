using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Queries.BrandGetAllQuery
{
    public class BrandGetAllRequest : IRequest<IEnumerable<Brand>>
    {
    }
}
