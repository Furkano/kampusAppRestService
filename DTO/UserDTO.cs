using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string? About { get; set; }
        public string ImageUrl { get; set; }
        public Role Role { get; set; }
        public City City { get; set; }
        public District District { get; set; }

        public UserContact UserContact { get; set; }
    }
}
