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
    public class SectorService
    {
        UnitOfWork _uow = null; 

        public SectorService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<SectorDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<Sector>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<SectorDTO>>();
        }
        public async Task<Sector> Get(int id)
        {
            return await _uow.Repository<Sector>().Get(id);
        }
        public async Task<Sector> Add(Sector sector)
        {
            return await _uow.Repository<Sector>().Insert(sector);
        }
        public void Update(Sector sector)
        {
            _uow.Repository<Sector>().Update(sector);
        }
        public void Delete(int id)
        {
            _uow.Repository<Sector>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
