using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Size : BaseEntity<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
