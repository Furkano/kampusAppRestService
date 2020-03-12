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
    public class EntriesController : ControllerBase
    {
        readonly EntryService _entryService = null;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public EntriesController()
        {
            _entryService = new EntryService();
            _logService = new LogService();
            _userService = new UserService();
        }

        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll( [FromQuery (Name="text")] string text, [FromQuery(Name = "page")] int page=1, [FromQuery(Name = "offset")] int offset = 20)
        {
            try
            {
                var entries = await _entryService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", entries, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir sorun oluştu." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var entry = await _entryService.Get(id);
                return new ResponceModel(200, "OK", entry, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] Entry model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var entry = await _entryService.Add(model);
                if (user.Id != entry.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await _entryService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", entry, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri eklenirken bir sorun oluştu." });
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
                _entryService.Delete(id);
                if (await _entryService.SaveChangesAsync())
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
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id, [FromBody] EntryDTO model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            if (id == 0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var entry = await _entryService.Get(id);
                if (entry == null)
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                if (user.Id != entry.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                entry = model.Adapt<Entry>();
                _entryService.Update(entry);
                if (await _entryService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", entry, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri güncellenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken bir sorun oluştu." });
            }
        }
    }
}