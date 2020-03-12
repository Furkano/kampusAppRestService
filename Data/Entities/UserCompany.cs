using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class UserCompany:BaseEntity
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public int CompanyTypeId { get; set; }
    }
}
