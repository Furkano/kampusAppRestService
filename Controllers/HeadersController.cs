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
    public class HeadersController : ControllerBase
    {
        readonly HeaderService _headerService = null;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public HeadersController()
        {
            _headerService = new HeaderService();
            _logService = new LogService();
            _userService = new UserService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll( [FromQuery(Name ="text")] string text, [FromQuery(Name = "page")] int page=1 , [FromQuery(Name = "offset")] int offset = 20)
        {
            try
            {
                var headers = await _headerService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", headers, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _headerService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var header = await _headerService.Get(id);
                return new ResponceModel(200, "OK", header, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _headerService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getirilirken bir sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add( [FromBody] Header model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var header = await _headerService.Add(model);
                if (user.Id != header.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await _headerService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", header, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri eklenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _headerService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri eklenirken bir sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update( [FromRoute] int id, [FromBody] HeaderDTO model)
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
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var header = await _headerService.Get(id);
                if (user.Id != header.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (header==null)
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                header = model.Adapt<Header>();
                _headerService.Update(header);
                if (await _headerService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", header, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri güncellenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _headerService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken bir sorun oluştu." });
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
            if (id == 0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "Silinecek veri bulunamadı." });
            }
            try
            {
                _headerService.Delete(id);
                if (await _headerService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri silinirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _headerService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir sorun oluştu." });
            }
        }
    }
}