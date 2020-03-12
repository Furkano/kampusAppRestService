using Microsoft.EntityFrameworkCore;
using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace noname.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private NonameContext _context;

        public NonameContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public Repository(NonameContext context)
        {
            _context = context;

        }


        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {

            return _context.Set<TEntity>().Where(predicate);
        }


        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);
        }
        public virtual void Update(TEntity entity)
        {
            var local = _context.Set<TEntity>()
         .Local
         .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            //_context.Entry(currentData).CurrentValues.SetValues(entity);
            // _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }


        public void Delete(int Id)
        {
            var o = _context.Set<TEntity>().AsNoTracking().Where(p => p.Id == Id).FirstOrDefault();
            _context.Set<TEntity>().Remove(o);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var repos = _context.Set<TEntity>().AsNoTracking().Where(predicate).ToList();
            foreach (var repo in repos)
            {
                _context.Set<TEntity>().Remove(repo);

            }
        }


    }
}
