using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostAdvertisementEstateDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int RoomTypeId { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int EstateTypeId { get; set; }
        public int HeatingTypeId { get; set; }
        public sbyte IsFurnished { get; set; }
        public decimal? Dues { get; set; }
        public string MapLat { get; set; }
        public string MapLong { get; set; }
        public int PostAdvertisementId { get; set; }

        public  PostAdvertisement PostAdvertisement { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public RoomType Roomtype { get; set; }
        public EstateType Estatetype { get; set; }
        public HeatingType Heatingtype { get; set; }

    }
}
