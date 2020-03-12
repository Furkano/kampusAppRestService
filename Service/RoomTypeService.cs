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
    public class RoomTypeService
    {
        UnitOfWork _uow = null;

        public RoomTypeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<RoomTypeDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<RoomType>()
                .Where(p => (text != null ? p.Name.Contains(text) : true))
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderBy(p => p.Name)
                .ToListAsync())
                .Adapt<List<RoomTypeDTO>>();
        }
        public async Task<RoomType> Get(int id)
        {
            return await _uow.Repository<RoomType>().Get(id);
        }
        public async Task<RoomType> Add(RoomType roomType)
        {
            return await _uow.Repository<RoomType>().Insert(roomType);
        }
        public void Update(RoomType roomType)
        {
            _uow.Repository<RoomType>().Update(roomType);
        }
        public void Delete(int id)
        {
            _uow.Repository<RoomType>().Delete(id);
        }
        public async Task<bool> SaveChanges()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
