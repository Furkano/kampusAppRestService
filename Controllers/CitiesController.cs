using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using noname.Data.Entities;
using noname.DTO;
using noname.Services;
using Mapster;
using noname.Service;
using Microsoft.AspNetCore.Authorization;

namespace noname.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitiesController:ControllerBase
    {
        readonly CityService cityService;
        LogService _logService = null;
        public CitiesController()
        {
            cityService = new CityService();
            _logService = new LogService();
        }

        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name ="text")] string text, [FromQuery(Name ="page")] int page=1, [FromQuery(Name ="offset")] int offset=20)
        {
            try
            {
                var cities = await cityService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", cities, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = cityService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Göneri getirilirken bir hata oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var city = await cityService.Get(id);
                return new ResponceModel(200, "OK", city, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = cityService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Göneri getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] City model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "Unauthorized", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var city = await cityService.Add(model);
                if (await cityService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", city, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Göneri gönderilirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = cityService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi eklenirken bir hata oluştu." });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ResponceModel> Delete(int id)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "Unauthorized", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                cityService.Delete(id);
                if (await cityService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Göneri silinirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = cityService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi silinirken bir hata oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id, [FromBody] CityDTO model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "Unauthorized", null, new string[] { "Yetkilendirme Hatası." });
            }
            if (id==0)
            {
                return new ResponceModel(404, "ERROR", null, new string[] { "Gönderilecek model bulunamadı" });
            }
            try
            {
                var city =await cityService.Get(id);
                if (city==null)
                {
                    return new ResponceModel(404, "ERROR", null, new string[] { "Gönderilecek model bulunamadı" });
                }
                city = model.Adapt<City>();
                cityService.Update(city);
                if (await cityService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", city, null);
                }
                else
                {
                    return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi güncellenirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = cityService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi güncellenirken bir hata oluştu." });
            }
        }
    }
}
