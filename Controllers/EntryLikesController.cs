using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class EntryLikesController : ControllerBase
    {
        readonly EntryLikeService _entryLikeService = null;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public EntryLikesController()
        {
            _entryLikeService = new EntryLikeService();
            _logService = new LogService();
            _userService = new UserService();
        }
        [HttpGet("{entryId}")]
        public async Task<ResponceModel> GetAll(int entryId)
        {
            try
            {
                var entryLikes = await _entryLikeService.GetAll(entryId);
                return new ResponceModel(200, "OK", entryLikes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryLikeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody]EntryLike model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var entryLike = await _entryLikeService.Add(model);
                if (user.Id != entryLike.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await _entryLikeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", entryLike, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri eklenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryLikeService.GetType().Name });
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
                _entryLikeService.Delete(id);
                if (await _entryLikeService.SaveChangesAsync())
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
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _entryLikeService.GetType().Name });
                return new ResponceModel(500, "ERRORr", null, new string[] { "Veri silinirken bir sorun oluştu." });
            }
        }
    }
}