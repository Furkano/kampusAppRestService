using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class StuffCategory:BaseEntity
    {
        public string Name { get; set; }
        public int UpperCategoryId { get; set; }
        public int CategoryOrder { get; set; }
    }
}
