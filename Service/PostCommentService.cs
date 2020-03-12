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
    public class PostCommentService
    {
        UnitOfWork _uow = null;
        public PostCommentService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<PostCommentDTO>> GetAll(int postId)
        {
            return (await _uow.Repository<PostComment>()
                .Where(p => p.PostId == postId)
                .Select(p => new PostCommentDTO { Body = p.Body, CreateDate = p.CreateDate, EditDate = p.EditDate, Id = p.Id, UserId = p.UserId, PostId = p.PostId, Username = p.User.Username })
                .ToListAsync());
        }
        public async Task<PostComment> Get(int id)
        {
            return await _uow.Repository<PostComment>().Get(id);
        }
        public async Task<PostComment> Add(PostComment postComment)
        {
            return await _uow.Repository<PostComment>().Insert(postComment);
        }
        public void Update(PostComment postComment)
        {
            _uow.Repository<PostComment>().Update(postComment);
        }
        public void Delete(int id)
        {
            _uow.Repository<PostComment>().Delete(id);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
