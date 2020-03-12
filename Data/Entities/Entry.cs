using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class Entry:BaseEntity
    {
        public string Body { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public int HeaderId { get; set; }

        public Header Header { get; set; }
        public User User { get; set; }
    }
}
