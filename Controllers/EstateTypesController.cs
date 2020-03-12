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
    public class EstateTypesController : ControllerBase
    {
        readonly EstateTypeService estateTypeService = null;
        readonly LogService _logService = null;
        public EstateTypesController()
        {
            estateTypeService = new EstateTypeService();
            _logService = new LogService();
        }
        [HttpGet("GetAll")]
        public async Task<ResponceModel> GetAll( [FromQuery (Name ="text")] string text, [FromQuery (Name = "page")] int page=1, [FromQuery(Name = "offset")] int offset=20)
        {
            try
            {
                var estateTypes = await estateTypeService.GetAll(text, page, offset);
                return new ResponceModel(200, "OK", estateTypes, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = estateTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veriler getitilirken sorun oluştu." });
            }
        }
        [HttpGet("{id}")]
        public async Task<ResponceModel> Get(int id)
        {
            try
            {
                var estateType = await estateTypeService.Get(id);
                return new ResponceModel(200, "OK", estateType, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = estateTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri getitilirken sorun oluştu." });
            }
        }
        [HttpPost]
        public async Task<ResponceModel> Add( [FromBody] EstateType model)
        {
            try
            {
                var estateType =await estateTypeService.Add(model);
                if (await estateTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", estateType, null);
                }
                else
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Veri eklenirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = estateTypeService.GetType().Name });
                return new ResponceModel(400, "ERROR", null, new string[] { "Veri eklenirken sorun oluştu." });
            }
        }
        [HttpPut("{id}")]
        public async Task<ResponceModel> Update( [FromRoute] int id, [FromBody] EstateTypeDTO model)
        {
            if (id==0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
            }
            try
            {
                var estateType = await estateTypeService.Get(id);
                if (estateType==null)
                {
                    return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
                }
                estateType = model.Adapt<EstateType>();
                estateTypeService.Update(estateType);
                if (await estateTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", estateType, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri güncellenirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = estateTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri güncellenirken sorun oluştu." });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ResponceModel> Delete([FromRoute] int id)
        {
            if (id == 0)
            {
                return new ResponceModel(404, "FAILD", null, new string[] { "Güncellenecek veri bulunamadı." });
            }
            try
            {
                estateTypeService.Delete(id);
                if (await estateTypeService.SaveChanges())
                {
                    return new ResponceModel(200, "OK", null, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Veri silinirken sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog() { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = estateTypeService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Veri silinirken sorun oluştu." });
            }
        }
        

        
    }
}