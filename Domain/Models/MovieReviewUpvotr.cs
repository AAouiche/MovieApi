using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MovieReviewUpvote
    {
        public string UserId { get; set; }
        public int ReviewId { get; set; }

        public ApplicationUser User { get; set; }
        public MovieReview Review { get; set; }
    }
}
