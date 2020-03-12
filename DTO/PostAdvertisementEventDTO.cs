using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostAdvertisementEventDTO
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DoorOpenDate { get; set; }
        public int PostAdvertisementId { get; set; }

        public PostAdvertisement PostAdvertisement { get; set; }
    }
}
