using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {

        public Image? Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<MovieReview>? Reviews { get; set; } = new List<MovieReview>();
        public ICollection<Movie>? WatchedMovies { get; set; } = new List<Movie>();
        public ICollection<MovieReviewUpvote>? UpvotedReviews { get; set; }
    }
}
