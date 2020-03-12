using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noname.Data.Entities;
using noname.DTO;
using noname.Service;

namespace noname.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenderTypesController : ControllerBase
    {
        GenderTypeService genderTypeService = null;
        readonly LogService _logService = null;
        public GenderTypesController()
        {
            genderTypeService = new GenderTypeService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name = "text")] string text, [FromQuery(Name = "page")]int page = 1, [FromQuery(Name = "offset")]int offset = 20)
        {
            try
            {
                var genderTypes = await genderTypeService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", genderTypes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = genderTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir hata oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get([FromRoute]int id)
        {
            try
            {
                var genderType = await genderTypeService.Get(id);
                return new ResponceModel(200, "OK", genderType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = genderTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] GenderType model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var genderType = await genderTypeService.Add(model);
                if (await genderTypeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", genderType, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri eklenirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = genderTypeService.GetType().Name });
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
            if (id==0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
            }
            try
            {
                var genderType = await genderTypeService.Get(id);
                if (genderType==null)
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                
                genderType= model.Adapt<GenderType>();
                genderTypeService.Update(genderType);
                if (await genderTypeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", genderType, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri güncellenirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = genderTypeService.GetType().Name });
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
                genderTypeService.Delete(id);
                if (await genderTypeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri silinirken bir hata oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = genderTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir hata oluştu." });
            }
        }
    }
}