
using System.Linq.Expressions;


namespace BulkyBooks.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        //T - Category

        T GetFirstOrDefailt(Expression<Func<T, bool>> filter, string? includeProperites = null);

        IEnumerable<T> GetAll(string? includeProperites = null);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
