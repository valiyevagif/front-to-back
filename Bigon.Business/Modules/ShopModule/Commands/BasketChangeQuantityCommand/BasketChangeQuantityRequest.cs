using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Commands.BasketChangeQuantityCommand
{
    public class BasketChangeQuantityRequest : IRequest<BasketSummary>
    {
        public int CatalogId { get; set; }
        public decimal Quantity { get; set; }
    }
}
