using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostAdvertisementResidenceDTO
    {
        public int Id { get; set; }
        public int ResidenceTypeId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public decimal Price { get; set; }
        public int GenderTypeId { get; set; }
        public int PersonCount { get; set; }
        public string MapLat { get; set; }
        public string MapLong { get; set; }
        public int PostAdvertisementId { get; set; }

        public ResidenceType Residencetype { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public GenderType GenderType { get; set; }
        public PostAdvertisement PostAdvertisement { get; set; }
    }
}
