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
    public class CityService
    {
        UnitOfWork _uow = null;

        public CityService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<CityDTO>> GetAll(string text,int page,int offset)
        {
            return (await _uow.Repository<City>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderByDescending(p => p.Id)
                .ToListAsync())
                .Adapt<List<CityDTO>>();
        }
        public async Task<City> Get(int id)
        {
            return await _uow.Repository<City>().Get(id);
        }
        public async Task<City> Add(City model)
        {
            return await _uow.Repository<City>().Insert(model);
        }
        public void Update(City model)
        {
            _uow.Repository<City>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<City>().Delete(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
