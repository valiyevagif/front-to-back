using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    public class FaqRepository : GeneralRepository<Faq>, IFaqRepository
    {
        public FaqRepository(DbContext db) : base(db)
        {
        }
    }
}
