using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class UserContact:BaseEntity
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int? Gsm { get; set; }
        public int? Phone { get; set; }
        public int? Instagram { get; set; }
        public int? Facebook { get; set; }
        public int? Twitter { get; set; }
        public int? Linkedin { get; set; }
        public int? Website { get; set; }

        public User User { get; set; }
    }
}
