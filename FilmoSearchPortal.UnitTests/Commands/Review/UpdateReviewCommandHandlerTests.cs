using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Review;
using FilmoSearchPortal.Application.DTO.Review;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using MediatR;
using Moq;
using Xunit;
using ReviewEntity = FilmoSearchPortal.Domain.Models.Review;

namespace FilmoSearchPortal.UnitTests.Commands.Review
{
    public class UpdateReviewCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IReviewRepository> _mockReviewRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateReviewCommandHandler _handler;

        public UpdateReviewCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockReviewRepository = new Mock<IReviewRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockRepositoryManager.Setup(x => x.ReviewRepository).Returns(_mockReviewRepository.Object);

            _handler = new UpdateReviewCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_UpdatesReview()
        {
            // Arrange
            var reviewId = 1;
            var reviewForUpdate = new ReviewForUpdateDto { Comment = "Updated comment", Stars = 4 };
            var reviewEntity = new ReviewEntity { Id = reviewId, UserId = "id", Comment = "Original comment", Stars = 5 };

            var command = new UpdateReviewCommand(reviewId, reviewForUpdate, false);

            _mockReviewRepository.Setup(x => x.GetReviewByIdAsync(reviewId, false, It.IsAny<CancellationToken>())).ReturnsAsync(reviewEntity);
            _mockMapper.Setup(x => x.Map(reviewForUpdate, reviewEntity));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockReviewRepository.Verify(x => x.GetReviewByIdAsync(reviewId, false, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(x => x.Map(reviewForUpdate, reviewEntity), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(Unit.Value, result);
        }
    }
}
