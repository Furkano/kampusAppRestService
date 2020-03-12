using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class HeaderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual UserDTO User { get; set; }
        public EntryDTO Entry { get; set; }
    }
}
