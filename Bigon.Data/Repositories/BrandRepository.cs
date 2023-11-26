using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    internal class BrandRepository : GeneralRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext db) : base(db)
        {
        }
    }
}
