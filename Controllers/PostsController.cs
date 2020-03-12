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
using noname.Services;

namespace noname.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PostsController : ControllerBase
    {
        readonly PostService postService;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public PostsController()
        {
            postService = new PostService();
            _logService = new LogService();
            _userService = new UserService();
        }

        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name = "text")] string text , [FromQuery(Name = "category")] int category, [FromQuery(Name = "page")] int page = 1,[FromQuery(Name = "offset")] int offset = 20)
        {
            try
            {
                var posts = await postService.GetAll(text, category, page, offset);

                return new ResponceModel(200, "OK", posts, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = postService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] {"Gönderililer getirilirken bir sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var post = await postService.Get(id);
                return new ResponceModel(200, "OK", post, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = postService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] Post model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var post = await postService.Add(model);
                if (user.Id != post.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await postService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", post, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Gönderi eklenirken bir sorun oluştu." });
                }
            }
            catch(Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = postService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi eklenirken bir hata oluştu." });
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
                postService.Delete(id);
                if (await postService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Gönderi silinirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = postService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi silinirken bir hata oluşru." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id,[FromBody] PostDTO model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            if (id==0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek gönderi bulamadı." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var post = await postService.Get(id);

                if (post==null)
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek gönderi bulamadı." });
                }
                if (user.Id != post.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                post = model.Adapt<Post>();

                postService.Update(post);
                if (await postService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Gönderi güncellenirken bir sorun oluştu."});
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = postService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Gönderi güncellenirken bir sorun oluştu." });
            }
        }

    }
}