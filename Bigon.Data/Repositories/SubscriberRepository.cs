using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    internal class SubscriberRepository : GeneralRepository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(DbContext db) : base(db)
        {
        }
    }
}
