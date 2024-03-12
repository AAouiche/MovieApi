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
        public string imdbID { get; set; } 
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        
        public ApplicationUser User { get; set; }
        public Movie Movie { get; set; }
        public ICollection<MovieReviewUpvote>? UpvotedByUsers { get; set; }
    }
}
