using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostAdvertisementResidence:BaseEntity
    {
        public int ResidenceTypeId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public decimal Price { get; set; }
        public int GenderTypeId { get; set; }
        public int PersonCount { get; set; }
        public int PostAdvertisementId { get; set; }
        public string MapLat { get; set; }
        public string MapLong { get; set; }
    }
}
