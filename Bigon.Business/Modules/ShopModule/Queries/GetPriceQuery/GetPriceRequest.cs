using MediatR;

namespace Bigon.Business.Modules.ShopModule.Queries.GetPriceQuery
{
    public class GetPriceRequest : IRequest<string>
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int MaterialId { get; set; }
    }
}
