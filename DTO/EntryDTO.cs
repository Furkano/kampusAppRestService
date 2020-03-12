using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class EntryDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public int HeaderId { get; set; }

        public HeaderDTO HeaderDTO { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
