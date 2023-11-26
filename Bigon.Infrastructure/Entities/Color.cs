using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Color : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
