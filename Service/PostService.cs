
using Mapster;
using Microsoft.EntityFrameworkCore;
using noname.Data;
using noname.Data.Entities;
using noname.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace noname.Services
{
    public class PostService
    {
        UnitOfWork _uow = null;

        public PostService()
        {
            _uow = new UnitOfWork();
        }

        public async Task<List<PostDTO>> GetAll(string text, int page, int offset)
        {
            return (await _uow.Repository<Post>()
                .Where(p => (text != null ? p.Body.Contains(text) : true))
                .Include(p => p.User)
                .Include(p => p.User.Role)
                .Skip((page - 1) * offset)
                .Take(offset)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync())
                .Adapt<List<PostDTO>>();

        }


        public async Task<Post> Get(int id)
        {
            return (await _uow.Repository<Post>().Get(id));
        }


        public async Task<Post> Add(Post model)
        {
            return await _uow.Repository<Post>().Insert(model);
        }

        public void Update(Post model)
        {
            _uow.Repository<Post>().Update(model);
            if (model.PostAdvertisement != null)
            {
                _uow.Repository<PostAdvertisement>().Update(model.PostAdvertisement);
                if (model.PostAdvertisement.PostAdvertisementEstate != null)
                {
                    _uow.Repository<PostAdvertisementEstate>().Update(model.PostAdvertisement.PostAdvertisementEstate);
                }
                if (model.PostAdvertisement.PostAdvertisementEvent != null)
                {
                    _uow.Repository<PostAdvertisementEvent>().Update(model.PostAdvertisement.PostAdvertisementEvent);
                }
                if (model.PostAdvertisement.PostAdvertisementJob != null)
                {
                    _uow.Repository<PostAdvertisementJob>().Update(model.PostAdvertisement.PostAdvertisementJob);
                }
                if (model.PostAdvertisement.PostAdvertisementResidence !=null)
                {
                    _uow.Repository<PostAdvertisementResidence>().Update(model.PostAdvertisement.PostAdvertisementResidence);
                }
                if (model.PostAdvertisement.PostAdvertisementStuff !=null)
                {
                    _uow.Repository<PostAdvertisementStuff>().Update(model.PostAdvertisement.PostAdvertisementStuff);
                }
            }

        }

        public void Delete(int id)
        {
            _uow.Repository<Post>().Delete(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }


    }
}
