using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostLikeDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public string Username { get; set; }
    }
}
