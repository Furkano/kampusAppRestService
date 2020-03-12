using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostFavorite:BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
