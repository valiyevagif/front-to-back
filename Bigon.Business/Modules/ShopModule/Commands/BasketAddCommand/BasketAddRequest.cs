using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Commands.BasketAddCommand
{
    public class BasketAddRequest : IRequest<BasketSummary>
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
    }
}
