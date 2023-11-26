using MediatR;

namespace Bigon.Business.Modules.ShopModule.Queries.BasketListQuery
{
    public class BasketListRequest : IRequest<IEnumerable<BasketListItem>>
    {
    }
}
