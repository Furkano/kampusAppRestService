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
    //[Authorize]
    public class ResidenceTypesController : ControllerBase
    {
        readonly ResidenceTypeService residenceTypeService = null;
        readonly LogService _logService = null;
        public ResidenceTypesController()
        {
            residenceTypeService = new ResidenceTypeService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name = "text")] string text, [FromQuery(Name = "page")] int page = 1, [FromQuery(Name = "offset")] int offset = 20)
        {
            try
            {
                var residenceTypes = await residenceTypeService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", residenceTypes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = residenceTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getitilirken sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var residenceType = await residenceTypeService.Get(id);
                return new ResponceModel(200, "OK", residenceType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = residenceTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getitilirken sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] ResidenceType model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var residenceType = await residenceTypeService.Add(model);
                if (await residenceTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", residenceType, null);
                }
                else
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Veri eklenirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = residenceTypeService.GetType().Name });
                return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id, [FromBody] ResidenceTypeDTO model)
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
                var residenceType = await residenceTypeService.Get(id);
                if (residenceType == null)
                {
                    return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                residenceType = model.Adapt<ResidenceType>();
                residenceTypeService.Update(residenceType);
                if (await residenceTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", residenceType, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri güncellenirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = residenceTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken sorun oluştu." });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ResponceModel> Delete([FromRoute] int id)
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
                residenceTypeService.Delete(id);
                if (await residenceTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri silinirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = residenceTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken sorun oluştu." });
            }
        }
    }
}