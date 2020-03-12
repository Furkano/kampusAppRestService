using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noname.Data.Entities;
using noname.DTO;
using noname.Service;

namespace noname.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HeatingTypesController : ControllerBase
    {
        HeatingTypeService heatingTypeService = null;
        readonly LogService _logService = null;
        public HeatingTypesController()
        {
            heatingTypeService = new HeatingTypeService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name = "text")] string text, [FromQuery(Name = "page")]int page = 1, [FromQuery(Name = "offset")]int offset = 20)
        {
            try
            {
                var heatingTypes = await heatingTypeService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", heatingTypes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = heatingTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir hata oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get([FromRoute]int id)
        {
            try
            {
                var heatingType = await heatingTypeService.Get(id);
                return new ResponceModel(200, "OK", heatingType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = heatingTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] HeatingType model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var heatingType = await heatingTypeService.Add(model);
                if (await heatingTypeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", heatingType, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = heatingTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri eklenirken bir hata oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id, [FromBody] GenderTypeDTO model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            if (id == 0)
            {
                return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı." });
            }
            try
            {
                var heatingType = await heatingTypeService.Get(id);
                if (heatingType == null)
                {
                    return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı." });
                }

                heatingType = model.Adapt<HeatingType>();
                heatingTypeService.Update(heatingType);
                if (await heatingTypeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", heatingType, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri güncellenirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = heatingTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken bir hata oluştu." });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ResponceModel> Delete(int id)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                heatingTypeService.Delete(id);
                if (await heatingTypeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri silinirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = heatingTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir hata oluştu." });
            }
        }
    }
}