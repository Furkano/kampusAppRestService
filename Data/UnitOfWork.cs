using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace noname.Data
{
    public class UnitOfWork:IDisposable
    {
        private NonameContext _context = null;

        public UnitOfWork()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            var dbContextOptions = new DbContextOptionsBuilder<NonameContext>();
            dbContextOptions.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            _context = new NonameContext(dbContextOptions.Options);

        }

        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }
            IRepository<TEntity> repository = new Repository<TEntity>(_context);
            repositories.Add(typeof(TEntity), repository);
            return repository;

        }


        public IQueryable<TEntity> Context<TEntity>() where TEntity : BaseEntity
        {
           return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();

        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
