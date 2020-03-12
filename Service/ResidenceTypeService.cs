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
    public class ResidenceTypeService
    {
        UnitOfWork _uow = null;

        public ResidenceTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<ResidenceTypeDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<ResidenceType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<ResidenceTypeDTO>>();
        }
        public async Task<ResidenceType> Get(int id)
        {
            return await _uow.Repository<ResidenceType>().Get(id);
        }
        public async Task<ResidenceType> Add(ResidenceType residenceype)
        {
            return await _uow.Repository<ResidenceType>().Insert(residenceype);
        }
        public void Update(ResidenceType residenceype)
        {
            _uow.Repository<ResidenceType>().Update(residenceype);
        }
        public void Delete(int id)
        {
            _uow.Repository<ResidenceType>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
