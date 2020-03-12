using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostImage:BaseEntity
    {
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
    }
}
