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
    public class CompanyTypeService
    {
        UnitOfWork _uow = null;
        public CompanyTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<CompanyTypeDTO>> GetAll(string text, int page,int offset)
        {
            return (await _uow.Repository<CompanyType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderByDescending(p => p.Name)
                .ToListAsync())
                .Adapt<List<CompanyTypeDTO>>();
        }
        public async Task<CompanyType> Get(int id)
        {
            return await _uow.Repository<CompanyType>().Get(id);
        }
        public async Task<CompanyType> Add(CompanyType model)
        {
            return await _uow.Repository<CompanyType>().Insert(model);
        }
        public void Update(CompanyType model)
        {
            _uow.Repository<CompanyType>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<CompanyType>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return await _uow.SaveChanges() > 0;
        }
    }
}
