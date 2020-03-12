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
    public class HeatingTypeService
    {
        UnitOfWork _uow = null;
        public HeatingTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<HeatingTypeDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<HeatingType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<HeatingTypeDTO>>();
        }
        public async Task<HeatingType> Get(int id)
        {
            return await _uow.Repository<HeatingType>().Get(id);
        }
        public async Task<HeatingType> Add(HeatingType model)
        {
            return await _uow.Repository<HeatingType>().Insert(model);
        }
        public void Update(HeatingType model)
        {
            _uow.Repository<HeatingType>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<HeatingType>().Delete(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
