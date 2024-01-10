using Application.Handlers.Review;
using Domain.Interfaces.IRepositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MovieReviewUnitTests
{
    public class DeleteMovieReviewHandlerTests
    {
        private readonly Mock<IMovieReviewRepository> _mockRepo;
        private readonly DeleteMovieReviewHandler _handler;

        public DeleteMovieReviewHandlerTests()
        {
            _mockRepo = new Mock<IMovieReviewRepository>();
            _handler = new DeleteMovieReviewHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task TestDeleteReview_Success()
        {
            // Arrange
            int validReviewId = 1;
            var command = new DeleteCommand { ReviewId = validReviewId };
            _mockRepo.Setup(repo => repo.DeleteReview(validReviewId))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.Success);
        }
    }
}
