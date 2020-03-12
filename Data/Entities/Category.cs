using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace noname.Data.Entities
{
    public partial class Category:BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("Parent")]
        public int UpperCategoryId { get; set; }
        public int CategoryOrder { get; set; }


        public virtual Category Parent { get; set; }
        //[InverseProperty("Id")]

        public virtual List<Category> Children { get; set; }

    }
}
