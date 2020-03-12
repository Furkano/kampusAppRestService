using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostAdvertisementEvent:BaseEntity
    {
        public int LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DoorOpenDate { get; set; }
        public int PostAdvertisementId { get; set; }
    }
}
