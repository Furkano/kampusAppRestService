using noname.Data;
using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.Service
{
    public class LogService
    {
        UnitOfWork _uow = null;
        public LogService()
        {
            _uow = new UnitOfWork();
        }

        public IQueryable<SystemLog> Get(int id)
        {
            return _uow.Repository<SystemLog>().Where(p => p.Id == id);
        }

        public async Task Add(SystemLog model)
        {
            await _uow.Repository<SystemLog>().Insert(model);
            await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
