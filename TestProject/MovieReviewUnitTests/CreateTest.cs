using Domain.Interfaces.IRepositories;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Handlers.Review.Create;

namespace TestProject.MovieReviewUnitTests
{
    public class CreateHandlerTests
    {
        private readonly Mock<IMovieReviewRepository> _mockRepo;
        private readonly CreateHandler _handler;

        public CreateHandlerTests()
        {
            _mockRepo = new Mock<IMovieReviewRepository>();
            _handler = new CreateHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task TestCreateReview_Success()
        {
            // Arrange
            var review = new MovieReview
            {
                ReviewId = 1, 
                UserId = "user123",
                MovieId = "movie456",
                Content = "Great movie, really enjoyed it!",
                Rating = 5,
                ReviewDate = DateTime.UtcNow,
                User = new ApplicationUser() 
            };
            var command = new CreateCommand { review = review };
            _mockRepo.Setup(repo => repo.CreateReview(It.IsAny<MovieReview>()))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.Success);
        }

    }
}
