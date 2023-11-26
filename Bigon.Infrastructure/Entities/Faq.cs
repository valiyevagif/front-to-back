using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Faq : BaseEntity<int>
    {
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
