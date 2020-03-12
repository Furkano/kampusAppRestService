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
    public class RoleService
    {
        UnitOfWork _uow = null;

        public RoleService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<RoleDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<Role>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<RoleDTO>>();
        }
        public async Task<Role> Get(int id)
        {
            return await _uow.Repository<Role>().Get(id);
        }
        public async Task<Role> Add(Role role)
        {
            return await _uow.Repository<Role>().Insert(role);
        }
        public void Update(Role role)
        {
            _uow.Repository<Role>().Update(role);
        }
        public void Delete(int id)
        {
            _uow.Repository<Role>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
