using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Return.DTO
{
    public class MovieReviewDTO
    {
        public int ReviewId { get; set; }
        public string? UserName { get; set; }
        public string imdbID { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public int UpVotes { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
