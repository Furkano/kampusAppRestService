using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class EntryLike:BaseEntity
    {
        public int EntryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public Entry Entry { get; set; }
        public User User { get; set; }
    }
}
