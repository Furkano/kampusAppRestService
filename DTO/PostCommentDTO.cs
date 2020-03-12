using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostCommentDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public string Username { get; set; }
    }
}
