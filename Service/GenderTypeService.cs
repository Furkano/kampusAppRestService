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
    public class GenderTypeService
    {
        UnitOfWork _uow = null;
        public GenderTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<GenderTypeDTO>> GetAll(string text,int page,int offset)
        {
            return (await _uow.Repository<GenderType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<GenderTypeDTO>>();
        }
        public async Task<GenderType> Get(int id)
        {
            return await _uow.Repository<GenderType>().Get(id);
        }
        public async Task<GenderType> Add(GenderType model)
        {
            return await _uow.Repository<GenderType>().Insert(model);
        }
        public void Update(GenderType model)
        {
            _uow.Repository<GenderType>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<GenderType>().Delete(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
