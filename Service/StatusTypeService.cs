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
    public class StatusTypeService
    {
        UnitOfWork _uow = null;

        public StatusTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<StatusTypeDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<StatusType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<StatusTypeDTO>>();
        }
        public async Task<StatusType> Get(int id)
        {
            return await _uow.Repository<StatusType>().Get(id);
        }
        public async Task<StatusType> Add(StatusType model)
        {
            return await _uow.Repository<StatusType>().Insert(model);
        }
        public void Update(StatusType model)
        {
            _uow.Repository<StatusType>().Update(model);
        }
        public void Delete(int id)
        {
            _uow.Repository<StatusType>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
