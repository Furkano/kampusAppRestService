using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostAdvertisementStuff:BaseEntity
    {
        public int StuffCategoryId { get; set; }
        public sbyte StatusTypeId { get; set; }
        public int PostAdvertisementId { get; set; }
        public decimal Price { get; set; }
    }
}
