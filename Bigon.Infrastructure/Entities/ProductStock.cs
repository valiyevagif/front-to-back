using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class ProductStock : BaseEntity<int>
    {
        public int CatalogId { get; set; }
        public string DocumentNo { get; set; }
        public decimal Quantity { get; set; }
    }
}
