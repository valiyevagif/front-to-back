using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    internal class CategoryRepository : GeneralRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(db)
        {
        }
    }
}
