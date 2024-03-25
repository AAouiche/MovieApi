using Application.Handlers.Account;
using Application.Handlers.Review;
using Domain.Models;
using Domain.Return.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieReviewController : BaseApiController
    {
        
         

        public MovieReviewController( ILogger<BaseApiController> logger) : base(logger)
        {
           
        }

        
        [HttpGet("getReviews/{imdbID}")]
        public async Task<IActionResult> GetReviews(string imdbID)
        {
            var query = new List.ListQuery { movieReviewId = imdbID };
            var result = await Mediator.Send(query);
            return HandleResults(result); 
        }

        
        [HttpPost("createReview")]
        public async Task<IActionResult> CreateReview([FromBody] MovieReviewDTO review)
        {
            var command = new Create.CreateCommand { review = review };
            var result = await Mediator.Send(command);
            return HandleResults(result);
        }

        
        [HttpPut("updateReview")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewUpdateDTO review)
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
        [HttpPost("upvoteReview/{reviewId}")]
        public async Task<IActionResult> UpvoteReview(int reviewId)
        {
            var command = new UpVote.UpvoteCommand { ReviewId = reviewId };
            var result = await Mediator.Send(command);
            return HandleResults(result);
        }

    }
}
