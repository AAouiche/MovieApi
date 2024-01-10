using Application.Handlers.Review;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MovieReviewUnitTests
{
    public class UpdateHandlerTests
    {
        private readonly Mock<IMovieReviewRepository> _mockRepo;
        private readonly UpdateHandler _handler;

        public UpdateHandlerTests()
        {
            _mockRepo = new Mock<IMovieReviewRepository>();
            _handler = new UpdateHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task TestUpdateReview_Success()
        {
            // Arrange
            var updatedReview = new MovieReview
            {
                ReviewId = 1,
                UserId = "user123Updated",
                MovieId = "movie456Updated",
                Content = "Updated review text.",
                Rating = 4,
                ReviewDate = DateTime.UtcNow
            };
            var command = new UpdateCommand { MovieReview = updatedReview };
            _mockRepo.Setup(repo => repo.UpdateReview(It.IsAny<MovieReview>()))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.Success);
        }
    }
}
