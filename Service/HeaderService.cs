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
    public class HeaderService
    {
        UnitOfWork _uow = null;
        public HeaderService()
        {
            _uow = new UnitOfWork();
        }
        public async Task<List<HeaderDTO>> GetAll(string text,int page,int offset)
        {
            return (await _uow.Repository<Header>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Include(p => p.User)
                .Include(p => p.Entry)
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<HeaderDTO>>();
        }
        public async Task<Header> Get(int id)
        {
            return await _uow.Repository<Header>().Get(id);
        }
        public async Task<Header> Add(Header model)
        {
            return await _uow.Repository<Header>().Insert(model);
        }
        public void Update(Header model)
        {
            _uow.Repository<Header>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<Header>().Delete(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
