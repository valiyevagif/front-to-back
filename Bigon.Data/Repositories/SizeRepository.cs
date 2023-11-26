using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    public class SizeRepository : GeneralRepository<Size>, ISizeRepository
    {
        public SizeRepository(DbContext db) : base(db)
        {
        }
    }
}
