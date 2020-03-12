using noname.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostAdvertisementDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Header { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public PostAdvertisementEstateDTO PostAdvertisementEstate { get; set; }
        public PostAdvertisementEventDTO PostAdvertisementEvent { get; set; }
        public PostAdvertisementJobDTO PostAdvertisementJob { get; set; }
        public PostAdvertisementResidenceDTO PostAdvertisementResidence { get; set; }
        public PostAdvertisementStuffDTO PostAdvertisementStuff { get; set; }
    }
}
