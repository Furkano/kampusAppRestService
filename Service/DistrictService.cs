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
    public class DistrictService
    {
        UnitOfWork _uow = null;
        public DistrictService()
        {
            _uow = new UnitOfWork();
        }
        public async Task<List<DistrictDTO>> GetAll(string text,int page,int offset)
        {
            return (await _uow.Repository<District>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Include(p => p.City)
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderByDescending(p => p.Name)
                .ToListAsync())
                .Adapt<List<DistrictDTO>>();
                
        }
        public async Task<District> Get(int id)
        {
            return await _uow.Repository<District>().Get(id);
        }
        public async Task<District> Add(District model)
        {
            return await _uow.Repository<District>().Insert(model);
        }
        public void Update(District model)
        {
            _uow.Repository<District>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<District>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
