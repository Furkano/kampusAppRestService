using Mapster;
using Microsoft.EntityFrameworkCore;
using noname.Data;
using noname.Data.Entities;
using noname.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.Service
{
    public class UserService
    {
        UnitOfWork _uow = null;
        public UserService()
        {
            _uow = new UnitOfWork();
        }
        public async Task<User> GetById(int id)
        {
            return (await _uow.Repository<User>()
                .Where(p => p.Id == id)
                .Select(p => new UserDTO { Role = p.Role, Name = p.Name, Username = p.Username, City = p.City, District = p.District, About = p.About, CreateDate = p.CreateDate, ImageUrl = p.ImageUrl, UserContact=p.UserContact })
                .FirstOrDefaultAsync())
                .Adapt<User>();
        }
        public async Task<User> GetUserByName(string text)
        {
            return (await _uow.Repository<User>().Where(p => p.Username == text).FirstOrDefaultAsync());
        }
        public async Task<UserDTO> Login(LoginDTO model)
        {
            return (await _uow.Repository<User>()
                .Where(p => p.Status == true && p.Username==model.Username && p.Password == model.Password)
                .Select(p => new UserDTO { Role = p.Role, Name = p.Name, Username = p.Username, City = p.City, District = p.District, About = p.About, CreateDate = p.CreateDate, ImageUrl = p.ImageUrl, UserContact = p.UserContact })
                .FirstOrDefaultAsync())
                .Adapt<UserDTO>();
        }
        public async Task<User> GetByActivityCode(string activationCode)
        {
            return (await _uow.Repository<User>().Where(p => p.ActivationCode == activationCode).FirstOrDefaultAsync());
        }
        public async Task<User> GetByEmail(string email)
        {
            return (await _uow.Repository<User>().Where(p => p.UserContact.Email == email).FirstOrDefaultAsync());
        }
        public async Task<bool> IsUser(string username,string email)
        {
            return (await _uow.Repository<User>().Where(p => p.Username == username || p.UserContact.Email == email).AnyAsync());
        }
        public async Task<User> Add(User user)
        {
            if (user!=null)
            {
                return await _uow.Repository<User>().Insert(user);
            }
            else
            {
                return null;
            }
        }
        public void Update(User user)
        {
            if (user!=null)
            {
                _uow.Repository<User>().Update(user);
            }
        }
        public void Delete(int id)
        {
            if (id!=0)
            {
                _uow.Repository<User>().Delete(id);
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _uow.SaveChanges()) > 0;
        }
    }
}
