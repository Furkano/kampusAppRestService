using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostAdvertisementEstate:BaseEntity
    {
        public decimal Price { get; set; }
        public int RoomTypeId { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int EstateTypeId { get; set; }
        public int HeatingTypeId { get; set; }
        public sbyte IsFurnished { get; set; }
        public decimal? Dues { get; set; }
        public int PostAdvertisementId { get; set; }
        public string MapLat { get; set; }
        public string MapLong { get; set; }
    }
}
