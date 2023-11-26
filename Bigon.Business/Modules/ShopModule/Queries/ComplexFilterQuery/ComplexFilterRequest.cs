using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Queries.ComplexFilterQuery
{
    public class ComplexFilterRequest : Pageable, IRequest<IPagedResponse<ComplexFilterResponseDto>>
    {
        public int[] Brands { get; set; }
        public int[] Colors { get; set; }
        public int[] Materials { get; set; }
        public int[] Sizes { get; set; }
        public ComplexFilterPrice Price { get; set; }
    }
}
