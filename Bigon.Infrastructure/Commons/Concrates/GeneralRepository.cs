using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bigon.Infrastructure.Commons.Concrates
{
    public abstract class GeneralRepository<T> : IRepository<T>
        where T : class
    {
        protected readonly DbContext db;
        private readonly DbSet<T> table;

        public GeneralRepository(DbContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }

        public T Add(T model)
        {
            table.Add(model);

            return model;
        }

        public T Edit(T model)
        {
            db.Entry(model).State = EntityState.Modified;
            return model;
        }

        public T Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null)
                return table.FirstOrDefault();

            var data = table.FirstOrDefault(predicate);

            if (data == null)
                throw new NotFoundException("RECORD_NOT_FOUND");

            return data;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, bool tracking = true)
        {
            var query = table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            return query;
        }

        public void Remove(T model)
        {
            table.Remove(model);
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
