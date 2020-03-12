using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostAdvertisementJobDTO
    {
        public int Id { get; set; }
        public int SectorId { get; set; }
        public int PostAdvertisementId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public PostAdvertisement PostAdvertisement { get; set; }
        public District District { get; set; }
        public City City { get; set; }
        public Sector Sector { get; set; }
    }
}
