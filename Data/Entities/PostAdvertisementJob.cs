using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostAdvertisementJob:BaseEntity
    {
        public int SectorId { get; set; }
        public int PostAdvertisementId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
    }
}
