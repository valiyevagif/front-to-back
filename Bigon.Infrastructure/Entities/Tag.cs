using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Tag : BaseEntity<int>
    {
        public string Text { get; set; }
    }
}
