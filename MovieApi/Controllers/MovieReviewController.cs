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

        
        [HttpGet("getReviews/{movieId}")]
        public async Task<IActionResult> GetReviews(string movieId)
        {
            var query = new List.ListQuery { movieReviewId = movieId };
            var result = await Mediator.Send(query);
            return Ok(result); 
        }

        
        [HttpPost("getReview")]
        public async Task<IActionResult> CreateReview([FromBody] MovieReview review)
        {
            var command = new Create.CreateCommand { review = review };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] MovieReview review)
        {
            var command = new UpdateCommand { MovieReview = review };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var command = new DeleteCommand { ReviewId = reviewId };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

       
    }
}
