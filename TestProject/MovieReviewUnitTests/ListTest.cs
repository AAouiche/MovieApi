using Domain.Interfaces.IRepositories;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Handlers.Review.List;

/*namespace TestProject.MovieReviewUnitTests
{
    *//*public class ListHandlerTests
    {
        private readonly Mock<IMovieReviewRepository> _mockRepo;
        private readonly ListHandler _handler;

        public ListHandlerTests()
        {
            _mockRepo = new Mock<IMovieReviewRepository>();
            _handler = new ListHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task TestListReviews_Success()
        {
            // Arrange
            string validMovieReviewId = "movie123";
            var query = new ListQuery { movieReviewId = validMovieReviewId };
            var mockReviews = new List<MovieReview>
        {
            new MovieReview {
                ReviewId = 1,
                UserId = "user123",
                imdbID = "movie456",
                Content = "Great movie, really enjoyed it!",
                Rating = 5,
                ReviewDate = DateTime.UtcNow,
                User = new ApplicationUser()  
            },
            
        };
            _mockRepo.Setup(repo => repo.GetReviews(validMovieReviewId))
                     .ReturnsAsync(mockReviews);

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            Assert.True(result.Success);
            Assert.Equal(mockReviews, result.Value);
        }*/
/*    }
}
*/