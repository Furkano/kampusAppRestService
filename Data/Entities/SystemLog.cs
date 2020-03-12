using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.Data.Entities
{
    public class SystemLog:BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public string EntityName { get; set; }
        public int UserId { get; set; }
    }
}
