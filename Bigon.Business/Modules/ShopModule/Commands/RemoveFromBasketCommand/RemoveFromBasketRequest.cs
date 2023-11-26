using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Commands.RemoveFromBasketCommand
{
    public class RemoveFromBasketRequest : IRequest<BasketSummary>
    {
        public int CatalogId { get; set; }
    }
}
