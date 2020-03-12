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
    public class PostCommentsController : ControllerBase
    {
        readonly PostCommentService _postCommentService = null;
        readonly LogService _logService = null;
        readonly UserService _userService = null;
        public PostCommentsController()
        {
            _postCommentService = new PostCommentService();
            _logService = new LogService();
            _userService = new UserService();
        }
        [HttpGet("GetAll/{postId}")]
        public async Task<ResponceModel> GetAll([FromRoute] int postId)
        {
            try
            {
                var postComments = await _postCommentService.GetAll(postId);
                return new ResponceModel(200, "OK", postComments, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postCommentService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getirilirken bir sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get([FromRoute] int id)
        {
            try
            {
                var postComment = await _postCommentService.Get(id);
                return new ResponceModel(200, "OK", postComment, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postCommentService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add([FromBody] PostComment model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var postComment = await _postCommentService.Add(model);
                if (user.Id != postComment.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (await _postCommentService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", postComment, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postCommentService.GetType().Name });
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
                _postCommentService.Delete(id);
                if (await _postCommentService.SaveChangesAsync())
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
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postCommentService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute] int id, [FromBody] PostCommentDTO model)
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
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                var postComment = await _postCommentService.Get(id);
                if (user.Id != postComment.UserId)
                {
                    return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
                }
                if (postComment == null)
                {
                    return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                postComment = model.Adapt<PostComment>();
                _postCommentService.Update(postComment);
                if (await _postCommentService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", postComment, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri güncellenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _postCommentService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken bir sorun oluştu." });
            }
        }

    }
}