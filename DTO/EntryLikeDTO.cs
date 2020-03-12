using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class EntryLikeDTO
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public string Username { get; set; }
    }
}
