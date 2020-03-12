using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace noname.Data
{
   public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> Get(int id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int Id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
