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
    public class EstateTypeService
    {
        UnitOfWork _uow = null;

        public EstateTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<EstateTypeDTO>> GetAll(string text,int page,int offset)
        {
            return (await _uow.Repository<EstateType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<EstateTypeDTO>>();
        }
        public async Task<EstateType> Get(int id)
        {
            return await _uow.Repository<EstateType>().Get(id);
        }
        public async Task<EstateType> Add(EstateType estateType)
        {
            return await _uow.Repository<EstateType>().Insert(estateType);
        }
        public void Update(EstateType estateType)
        {
            _uow.Repository<EstateType>().Update(estateType);
        }
        public void Delete(int id)
        {
            _uow.Repository<EstateType>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
