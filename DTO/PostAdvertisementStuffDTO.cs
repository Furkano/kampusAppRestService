using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostAdvertisementStuffDTO
    {
        public int Id { get; set; }
        public int StuffCategoryId { get; set; }
        public sbyte StatusTypeId { get; set; }
        public int PostAdvertisementId { get; set; }
        public decimal Price { get; set; }

        public StuffCategory StuffCategory { get; set; }
        public StatusType StatusType { get; set; }
        public PostAdvertisement PostAdvertisement { get; set; }
    }
}
