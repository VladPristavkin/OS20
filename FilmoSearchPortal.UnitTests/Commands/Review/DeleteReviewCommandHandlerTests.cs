using FilmoSearchPortal.Application.CQRS.Commands.Review;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using MediatR;
using Moq;
using Xunit;
using ReviewEntity = FilmoSearchPortal.Domain.Models.Review;

namespace FilmoSearchPortal.UnitTests.Commands.Review
{
    public class DeleteReviewCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IReviewRepository> _mockReviewRepository;
        private readonly DeleteReviewCommandHandler _handler;

        public DeleteReviewCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockReviewRepository = new Mock<IReviewRepository>();

            _mockRepositoryManager.Setup(x => x.ReviewRepository).Returns(_mockReviewRepository.Object);

            _handler = new DeleteReviewCommandHandler(_mockRepositoryManager.Object);
        }

        [Fact]
        public async Task Handle_DeletesReview()
        {
            // Arrange
            var reviewId = 1;
            var review = new ReviewEntity { Id = reviewId, Stars = 2, UserId = "id", Comment = "Sample review" };
            var command = new DeleteReviewCommand(reviewId, false);

            _mockReviewRepository.Setup(x => x.GetReviewByIdAsync(reviewId, false, It.IsAny<CancellationToken>())).ReturnsAsync(review);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockReviewRepository.Verify(x => x.GetReviewByIdAsync(reviewId, false, It.IsAny<CancellationToken>()), Times.Once);
            _mockReviewRepository.Verify(x => x.DeleteReview(review), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(Unit.Value, result);
        }
    }
}
