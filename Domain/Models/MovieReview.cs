using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MovieReview
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; } 
        public string MovieId { get; set; } 
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }

        public ApplicationUser User { get; set; }
        public Movie Movie { get; set; }
    }
}
