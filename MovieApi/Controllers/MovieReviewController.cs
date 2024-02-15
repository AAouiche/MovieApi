using Application.Handlers.Account;
using Application.Handlers.Review;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieReviewController : BaseApiController
    {
        
         

        public MovieReviewController( ILogger<BaseApiController> logger) : base(logger)
        {
           
        }

        
        [HttpGet("getReviews/{imdbID}")]
        public async Task<IActionResult> GetReviews(string MovieId)
        {
            var query = new List.ListQuery { movieReviewId = MovieId };
            var result = await Mediator.Send(query);
            return HandleResults(result); 
        }

        
        [HttpPost("getReview")]
        public async Task<IActionResult> CreateReview([FromBody] MovieReview review)
        {
            var command = new Create.CreateCommand { review = review };
            var result = await Mediator.Send(command);
            return HandleResults(result);
        }

        
        [HttpPut("updateReview")]
        public async Task<IActionResult> UpdateReview([FromBody] MovieReview review)
        {
            var command = new UpdateCommand { MovieReview = review };
            var result = await Mediator.Send(command);
            return HandleResults(result);
        }

        
        [HttpDelete("deleteReview/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var command = new DeleteCommand { ReviewId = reviewId };
            var result = await Mediator.Send(command);
            return HandleResults(result);
        }
        [HttpGet("listWatchedMovies")]
        public async Task<IActionResult> ListWatchedMovies()
        {
            var query = new ListWatchedMovie.ListWatchedMoviesQuery();
            var result = await Mediator.Send(query);

            return HandleResults(result);
        }
        [HttpDelete("deleteWatchedMovie/{imdbID}")]
        public async Task<IActionResult> DeleteWatchedMovie(string MovieId)
        {
            var command = new DeleteWatchedMovie.DeleteWatchedMovieCommand { MovieId = MovieId };
            var result = await Mediator.Send(command);

            return HandleResults(result);
        }
    }
}
