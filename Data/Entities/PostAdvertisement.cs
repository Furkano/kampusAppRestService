using System;
using System.Collections.Generic;

namespace noname.Data.Entities
{
    public partial class PostAdvertisement:BaseEntity
    {
        public int PostId { get; set; }
        public string Header { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public PostAdvertisementEstate PostAdvertisementEstate { get; set; }
        public PostAdvertisementEvent PostAdvertisementEvent { get; set; }
        public PostAdvertisementJob PostAdvertisementJob { get; set; }
        public PostAdvertisementResidence PostAdvertisementResidence { get; set; }
        public PostAdvertisementStuff PostAdvertisementStuff { get; set; }
    }
}
