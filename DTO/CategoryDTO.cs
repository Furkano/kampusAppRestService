using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CategoryDTO> Children { get; set; }
        public int AdvertisementCount { get; set; }
    }
}
