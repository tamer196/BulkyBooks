using BulkyBooks.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBooks.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BulkyDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(BulkyDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //includeProp - Insert the Proprites in the correct syntax {Category,CoverType}
        public IEnumerable<T> GetAll(string? includeProperites = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperites != null)
            {
                foreach (var includeProperite in includeProperites.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query= query.Include(includeProperite);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefailt(Expression<Func<T, bool>> filter, string? includeProperites = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (includeProperites != null)
            {
                foreach (var includeProperite in includeProperites.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperite);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
