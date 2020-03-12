using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class District : BaseEntity
    {
        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual City City { get; set; }
    }
}
