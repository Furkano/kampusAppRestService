using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using noname.Data.Entities;
using noname.DTO;
using noname.Service;

namespace noname.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        readonly CategoryService _categoryService;
        readonly LogService _logService;
        public CategoriesController()
        {
            _categoryService = new CategoryService();
            _logService = new LogService();
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var result = new { breadcrumb = _categoryService.GetParent(id), category = (await _categoryService.Get(id).FirstOrDefaultAsync()).Adapt<CategoryDTO>(), subcategories = (_categoryService.GetByUpper(id)) };
                return new ResponceModel(200, "OK", result, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _categoryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
            }
        }
        [HttpGet("GetHeaderCategories")]
        public async Task<ResponceModel> GetHeaderCategories()
        {
            try
            {
                var result = _categoryService.GetByOrder().ToListAsync().Adapt<List<CategoryDTO>>();
                return new ResponceModel(200, "OK", result, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _categoryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
                
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] Category model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier==null)
            {
                return new ResponceModel(401, "FAILD", null, new string[] { "Yetkilendirme hatası." });
            }
            try
            {
                var add = await _categoryService.Add(model);
                if (await _categoryService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", add, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILED", null, new string[] { "Kategori ekleme sırasında bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, EntityName = _categoryService.GetType().Name, UserId = 0 });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Put( [FromRoute] int id, [FromBody] Category model)
        {
            if (id!=model.Id)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "İçerik bulunamadı." });
            }
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier==null)
            {
                return new ResponceModel(401, "FAILD", null, new string[] { "Yetkilendirme hatası." });
            }
            try
            {
                _categoryService.Update(model);
                if (await _categoryService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", model, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILED", null, new string[] { "Kategori güncelleme sırasında bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, EntityName = _categoryService.GetType().Name, UserId = 0 });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
            }
        }

    }
}