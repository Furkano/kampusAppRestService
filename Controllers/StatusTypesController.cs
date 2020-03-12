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
    public class StatusTypesController : ControllerBase
    {
        readonly StatusTypeService statusTypeService = null;
        readonly LogService _logService = null;
        public StatusTypesController()
        {
            statusTypeService = new StatusTypeService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name = "text")] string text, [FromQuery(Name = "page")] int page = 1, [FromQuery(Name = "offset")] int offset = 20)
        {
            try
            {
                var statusType = await statusTypeService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", statusType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = statusTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getitilirken sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var statusType = await statusTypeService.Get(id);
                return new ResponceModel(200, "OK", statusType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = statusTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getitilirken sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] StatusType model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var statusType = await statusTypeService.Add(model);
                if (await statusTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", statusType, null);
                }
                else
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Veri eklenirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = statusTypeService.GetType().Name });
                return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id, [FromBody] StatusTypeDTO model)
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
                var statusType = await statusTypeService.Get(id);
                if (statusType == null)
                {
                    return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                statusType = model.Adapt<StatusType>();
                statusTypeService.Update(statusType);
                if (await statusTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", statusType, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri güncellenirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = statusTypeService.GetType().Name });
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
                statusTypeService.Delete(id);
                if (await statusTypeService.SaveChanges())
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
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = statusTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken sorun oluştu." });
            }
        }
    }
}