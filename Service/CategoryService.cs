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
    public class CategoryService
    {
        UnitOfWork _uow = null;

        public CategoryService()
        {
            _uow = new UnitOfWork();
        }

        public List<CategoryDTO> GetParent(int id)
        {
            List<Category> categories = new List<Category>();
            int categoryId = id;
        tag:
            var category = _uow.Repository<Category>().Where(p => p.Id == categoryId).FirstOrDefault();
            if (category !=null)
            {
                categories.Add(category);
                if (category.UpperCategoryId != 0)
                {
                    categoryId = category.UpperCategoryId;
                    goto tag;
                }
            }
            categories.Reverse();

            return categories.Adapt<List<CategoryDTO>>();
        }
        public IQueryable<object> GetByUpper(int id)
        {
            var result = _uow.Repository<Category>()
                .Where(p => p.UpperCategoryId == id)
                .AsNoTracking()
                .Select(p => new CategoryDTO
                {
                    Name=p.Name, AdvertisementCount=0,Id=p.Id, Children=p.Children
                        .Select(s=>new CategoryDTO
                        {
                            Name=s.Name,AdvertisementCount=0,Id=s.Id,Children=s.Children
                                .Select(s2=> new CategoryDTO
                                {
                                    Name = s2.Name,AdvertisementCount = 0,Id = s2.Id, Children = s2.Children
                                        .Select(s3=> new CategoryDTO { 
                                            Name = s3.Name, AdvertisementCount = 0, Id = s3.Id 
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                });
            return result;
        }
        public IQueryable<Category> GetByOrder()
        {
            return _uow.Repository<Category>().Where(p => p.UpperCategoryId == 0 && p.CategoryOrder == 1).Include(p => p.Children).ThenInclude(p => p.Children);
        }
        public IQueryable<Category> GetSubCategories(int id)
        {
            return _uow.Repository<Category>().Where(p => p.UpperCategoryId == id);
        }
        public IQueryable<Category> Get(int id)
        {

            return _uow.Repository<Category>().Where(p => p.Id == id);
        }
        public async Task<Category> Add(Category model)
        {
            return await _uow.Repository<Category>().Insert(model);
        }

        public void Update(Category model)
        {
            _uow.Repository<Category>().Update(model);
        }

        public void Delete(int id)
        {
            _uow.Repository<Category>().Delete(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
