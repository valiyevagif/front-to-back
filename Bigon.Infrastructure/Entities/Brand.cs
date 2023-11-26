using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Brand : BaseEntity<byte>
    {
        public string Name { get; set; }
    }
}
