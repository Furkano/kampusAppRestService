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
    //[Authorize]
    public class PostLikesController : ControllerBase
    {
        readonly PostLikeService _postLikeService = null;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public PostLikesController()
        {
            _postLikeService = new PostLikeService();
            _logService = new LogService();
            _userService = new UserService();
        }
        [HttpGet("entryId")]
        public async Task<ResponceModel> GetAll(int postId)
        {
            try
            {
                var postLikes = await _postLikeService.GetAll(postId);
                return new ResponceModel(200, "OK", postLikes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postLikeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add( [FromBody] PostLike model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var postLike = await _postLikeService.Add(model);
                if (user.Id != postLike.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await _postLikeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", postLike, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postLikeService.GetType().Name });
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
                return new ResponceModel(404, "ERROR", null, new string[] { "Silinecek veri bulunamadı." });
            }
            try
            {
                _postLikeService.Delete(id);
                if (await _postLikeService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri silinirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postLikeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir sorun oluştu." });
            }
        }
    }
}