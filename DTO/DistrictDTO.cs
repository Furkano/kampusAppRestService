using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class DistrictDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

        public CityDTO City { get; set; }
    }
}
