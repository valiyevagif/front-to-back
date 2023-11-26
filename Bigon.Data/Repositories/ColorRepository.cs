using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    public class ColorRepository : GeneralRepository<Color>, IColorRepository
    {
        public ColorRepository(DbContext db) : base(db)
        {
        }
    }
}
