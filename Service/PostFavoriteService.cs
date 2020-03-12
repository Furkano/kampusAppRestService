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
    public class PostFavoriteService
    {
        UnitOfWork _uow = null;

        public PostFavoriteService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<PostFavoriteDTO>> GetAll(int userId,int page,int offset)
        {
            return await _uow.Repository<PostFavorite>()
                .Where(p => p.UserId == userId)
                .Select(p => new PostFavoriteDTO { PostDTO = (p.Post).Adapt<PostDTO>(), PostUsername=p.Post.User.Username, PostUserId = p.User.Id, CreateDate=p.CreateDate})
                .Skip((page-1)*offset)
                .Take(offset)
                .OrderByDescending(p=>p.CreateDate)
                .ToListAsync();
        }
        public async Task<PostFavorite> Add(PostFavorite postFavorite)
        {
            return await _uow.Repository<PostFavorite>().Insert(postFavorite);
        }

        public void Delete(int id)
        {
            _uow.Repository<PostFavorite>().Delete(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
