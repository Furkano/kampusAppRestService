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
    public class PostLikeService
    {
        UnitOfWork _uow = null;
        public PostLikeService()
        {
            _uow = new UnitOfWork();
        }
        public async Task<List<PostLikeDTO>> GetAll(int postId)
        {
            return (await _uow.Repository<PostLike>()
                .Where(p => p.PostId == postId)
                .Select(p => new PostLikeDTO() { Id = p.Id, Username = p.User.Name, CreateDate = p.CreateDate, PostId = p.PostId, UserId = p.UserId })
                .ToListAsync());

        }
        public async Task<PostLike> Add(PostLike model)
        {
            return await _uow.Repository<PostLike>().Insert(model);
        }

        public void Delete(int id)
        {
            _uow.Repository<PostLike>().Delete(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
