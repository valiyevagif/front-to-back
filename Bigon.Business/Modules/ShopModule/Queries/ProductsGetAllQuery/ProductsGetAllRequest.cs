using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Commons.Concrates;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductsGetAllQuery
{
    public class ProductsGetAllRequest : Pageable, IRequest<IPagedResponse<ProductGetAllDto>>
    {
        public override int Size
        {
            get
            {
                return base.Size < 12 ? 12 : base.Size;
            }
            set
            {
                base.Size = value < 12 ? 12 : value;
            }
        }
    }
}
