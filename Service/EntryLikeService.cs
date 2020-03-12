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
    public class EntryLikeService
    {
        UnitOfWork _uow = null;
        public EntryLikeService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<EntryLikeDTO>> GetAll(int  entryId)
        {
            return (await _uow.Repository<EntryLike>()
                .Where(p => p.EntryId == entryId)
                .Select(p => new EntryLikeDTO() { Id = p.Id, Username = p.User.Name, CreateDate = p.CreateDate, EntryId = p.EntryId,UserId=p.UserId })
                .ToListAsync());
                
        }
        public async Task<EntryLike> Add(EntryLike entryLike)
        {
            return await _uow.Repository<EntryLike>().Insert(entryLike);
        }
        public void Delete(int id)
        {
            _uow.Repository<EntryLike>().Delete(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
