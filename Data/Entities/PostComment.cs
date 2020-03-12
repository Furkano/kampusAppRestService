using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostComment:BaseEntity
    {
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
