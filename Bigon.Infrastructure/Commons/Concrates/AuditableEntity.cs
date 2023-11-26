using Bigon.Infrastructure.Commons.Abstracts;

namespace Bigon.Infrastructure.Commons.Concrates
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}