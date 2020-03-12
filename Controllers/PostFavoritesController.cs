using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PostFavoritesController : ControllerBase
    {
        readonly PostFavoriteService _postFavoriteService = null;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public PostFavoritesController()
        {
            _postFavoriteService = new PostFavoriteService();
            _logService = new LogService();
            _userService = new UserService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll( [FromQuery (Name ="userId")] int userId,[FromQuery (Name ="page")] int page=1, [FromQuery(Name = "offset")] int offset=20)
        {
            try
            {
                var postFavorites = await _postFavoriteService.GetAll(userId,page,offset);
                return new ResponceModel(200, "OK", postFavorites, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postFavoriteService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir sorun oluştu." });
            }
        }
 
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] PostFavorite model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var postFavorite = await _postFavoriteService.Add(model);
                if (user.Id != postFavorite.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await _postFavoriteService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", postFavorite, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postFavoriteService.GetType().Name });
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
                _postFavoriteService.Delete(id);
                if (await _postFavoriteService.SaveChangesAsync())
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
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postFavoriteService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir sorun oluştu." });
            }
        }
    }
}