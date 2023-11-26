using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
