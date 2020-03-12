using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class Header:BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
        public Entry Entry { get; set; }
    }
}
