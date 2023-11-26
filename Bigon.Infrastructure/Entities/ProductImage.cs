using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class ProductImage : BaseEntity<int>
    {
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }

    }
}
