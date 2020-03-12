using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostLike:BaseEntity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
