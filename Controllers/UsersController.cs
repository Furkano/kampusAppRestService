using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using noname.ConfigServices;
using noname.Data.Entities;
using noname.DTO;
using noname.Service;

namespace noname.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        UserService _userService = null;
        LogService _logService = null;
        public UsersController()
        {
            _userService = new UserService();
            _logService = new LogService();
        }

        [HttpGet]
        public async Task<ResponceModel> Get()
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier==null)
            {
                return new ResponceModel(401, "Unauthorized", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value))).Adapt<UserDTO>();
                return new ResponceModel(200, "OK", user, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _userService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
            }
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ResponceModel> Register([FromBody] RegisterDTO model)
        {
            try
            {
                var isExist = await _userService.GetUserByName(model.Username);
                if (isExist != null)
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Bu İsimde bir kullanıcı bulunmaktadır." });
                }
                var user = new User();
                user.ActivationCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15);
                user.Password = getHash(model.Password);
                user.Name = model.Name;
                
                user.RoleId = 1;
                user.Status = true;
                user.Username = model.Username;
                user.CityId = model.CityId;
                user.isCompany = false;
                user.CreateDate = DateTime.UtcNow;
                user.ImageUrl = "clikclikcliklik";
                await _userService.Add(user);
                if (await _userService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", user, null);
                }
                else
                {
                    return new ResponceModel(400, "FAILD", null, new string[] { "Kullanıcı yaratılırken bir sorun oluştu." });
                }

            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _userService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ResponceModel> Login( [FromBody] LoginDTO model)
        {
            try
            {
                model.Password = getHash(model.Password);
                var user = await _userService.Login(model);
                if (user==null)
                {
                    return new ResponceModel(401, "Unauthorized", null, new string[] { "Bilgilerinizi kontrol edinip tekrar deneyiniz." });
                }
                var claims = new[] { new Claim("id", user.Id.ToString()) };

                var token = new JwtSecurityToken
                    (
                        issuer: "furkansoysal17",
                        audience: "furkansoysal17",
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("furkan-soysal-17-furkan-soysal-17")),
                            SecurityAlgorithms.HmacSha256)
                    );
                var result = new { user = user, token = new JwtSecurityTokenHandler().WriteToken(token) };
                return new ResponceModel(200, "OK", result, null);
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _userService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." });
            }
        }
        [HttpPost("updatePassword")]
        public async Task<ResponceModel> RePassword( [FromBody] RePasswordDTO model)
        {
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier==null)
            {
                return new ResponceModel(401, "Unauthorized", null, new string[] { "Yetkilendirme Hatası." });
            }
            try
            {
                var user = (await _userService.GetById(int.Parse(identifier.Value)));
                if (user.Password!=getHash(model.CurrentPassword))
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Mevcut Şifreniz yanlış. Tekrar deneyiniz." });
                }
                user.Password = getHash(model.Password);
                _userService.Update(user);

                if (await _userService.SaveChangesAsync())
                {
                    return new ResponceModel(200, "OK", user, null);
                }
                else
                {
                    return new ResponceModel(400, "ERROR", null, new string[] { "Şifre güncellenirken bir sorun oluştu." });
                }
            }
            catch (Exception ex)
            {
                await _logService.Add(new SystemLog { Content = ex.Message, CreateDate = DateTime.Now, UserId = 0, EntityName = _userService.GetType().Name });
                return new ResponceModel(500, "ERROR", null, new string[] { "Bir sorun oluştu." }); 
            }
        }


        private string getHash(string text)
        {   // SHA512 is disposable by inheritance.
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}