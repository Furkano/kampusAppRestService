using Mapster;
using Microsoft.EntityFrameworkCore;
using noname.Data;
using noname.Data.Entities;
using noname.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.Service
{
    public class EntryService
    {
        UnitOfWork _uow = null;

        public EntryService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<EntryDTO>> GetAll(string text,int page,int offset)
        {
            return (await _uow.Repository<Entry>()
                .Where(p => (text != null ? p.Body.Contains(text) : true))
                .Include(p => p.Header)
                .Include(p => p.User)
                .Skip((page - 1) * offset)
                .Take(offset)
                .ToListAsync())
                .Adapt<List<EntryDTO>>();
        }
        public async Task<Entry> Get(int id)
        {
            return await _uow.Repository<Entry>().Get(id);
        }
        public async Task<Entry> Add(Entry model)
        {
            return await _uow.Repository<Entry>().Insert(model);
        }
        public void Update(Entry model)
        {
            _uow.Repository<Entry>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<Entry>().Delete(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
