using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    public class MaterialRepository : GeneralRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(DbContext db) : base(db)
        {
        }
    }
}
