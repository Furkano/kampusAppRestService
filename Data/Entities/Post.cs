using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace noname.Data.Entities
{
    public partial class Post:BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        public virtual User User { get; set; }
        public virtual List<PostImage> PostImages { get; set; }
        public  PostAdvertisement PostAdvertisement { get; set; }
    }
}
