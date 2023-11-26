namespace Bigon.Infrastructure.Commons.Concrates
{
    public abstract class BaseEntity<TKey> : AuditableEntity
        where TKey : struct
    {
        public TKey Id { get; set; }
    }
}