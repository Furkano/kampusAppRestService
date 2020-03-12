using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noname.Data.Entities;
using noname.DTO;
using noname.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyTypesController:ControllerBase
    {
        readonly CompanyTypeService companyTypeService = null;
        readonly LogService _logService = null;
        public CompanyTypesController()
        {
            companyTypeService = new CompanyTypeService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll([FromQuery(Name="text")] string text, [FromQuery (Name ="page")] int page=1, [FromQuery(Name ="offset")] int offset=20)
        {
            try
            {
                var companyTypes = await companyTypeService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", companyTypes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = companyTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Şirketler getirilirken bir hata oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var companyType = await companyTypeService.Get(id);
                return new ResponceModel(200, "OK", companyType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = companyTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Şirket getirilirken bir hata oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add( [FromBody] CompanyType model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var companyType = await companyTypeService.Add(model);
                if (await companyTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", companyType, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Şirket kayıt edilirken bir hata oluştu" });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = companyTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Şirket kayıt edilirken bir hata oluştu" });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update([FromRoute]int id,[FromBody]CompanyTypeDTO model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier == null)
            {
                return new ResponceModel(401, "FAILED", null, new string[] { "Yetkilendirme Hatası." });
            }
            if (id==0)
            {
                return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı" });
            }
            try
            {
                var companyType = await companyTypeService.Get(id);
                if (companyType==null)
                {
                    return new ResponceModel(404, "ERROR", null, new string[] { "Güncellenecek veri bulunamadı" });
                }
                companyType = model.Adapt<CompanyType>();
                companyTypeService.Update(companyType);
                if (await companyTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri güncellenirken bir hata oluştu" });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = companyTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken bir hata oluştu" });
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
                companyTypeService.Delete(id);
                if (await companyTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Veri silinirken bir hata oluştu" });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = companyTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken bir hata oluştu" });
            }
        }
    }
}
