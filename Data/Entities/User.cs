using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class User:BaseEntity
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string ActivationCode { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string? About { get; set; }
        public string ImageUrl { get; set; }
        public int CityId { get; set; }
        public int? DistrictId { get; set; }
        public bool Status { get; set; }
        public bool isCompany { get; set; }

        public Role Role { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public UserContact UserContact { get; set; }
    }
}
