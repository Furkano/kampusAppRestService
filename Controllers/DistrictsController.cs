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
    public class DistrictController : ControllerBase
    {
        DistrictService districtService = null;
        readonly LogService _logService = null;
        public DistrictController()
        {
            districtService = new DistrictService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name ="text")]string text,[FromQuery(Name ="page")]int page=1,[FromQuery(Name ="offset")] int offset=20)
        {
            try
            {
                var districts = await districtService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", districts, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = districtService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "İlçeler getirilirken bir sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get([FromRoute] int id)
        {
            try
            {
                var district = await districtService.Get(id);
                return new ResponceModel(200, "OK", district, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = districtService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "İlçe getirilirken bir sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] District model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var district = await districtService.Add(model);
                if (await districtService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", district, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "İlçe kayıt edilirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = districtService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "İlçe kayıt edilirken bir sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id,[FromBody] District model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            if (id==0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "İlçe güncellenirken bir sorun oluştu." });
            }
            try
            {
                var district = await districtService.Get(id);
                if (district==null)
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "İlçe güncellenirken bir sorun oluştu." });
                }
                district = model.Adapt<District>();
                districtService.Update(model);
                if (await districtService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", district, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "İlçe güncellenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = districtService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "İlçe güncellenirken bir sorun oluştu." });
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
                districtService.Delete(id);
                if (await districtService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "İlçe silinirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = districtService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "İlçe silinirken bir sorun oluştu." });
            }
        }
    }
}