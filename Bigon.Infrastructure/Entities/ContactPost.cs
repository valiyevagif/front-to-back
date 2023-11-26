using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class ContactPost : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Answer { get; set; }
        public int? AnsweredBy { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }
}
