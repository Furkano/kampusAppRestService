using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noname.DTO
{
    public class PostFavoriteDTO
    {

        public int  PostUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string PostUsername { get; set; }
        public PostDTO PostDTO { get; set; }

    }
}
