using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Review;
using FilmoSearchPortal.Application.DTO.Review;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using FilmEntity = FilmoSearchPortal.Domain.Models.Film;
using ReviewEntity = FilmoSearchPortal.Domain.Models.Review;

namespace FilmoSearchPortal.UnitTests.Commands.Review
{
    public class CreateReviewCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IFilmRepository> _mockFilmRepository;
        private readonly Mock<IReviewRepository> _mockReviewRepository;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateReviewCommandHandler _handler;

        public CreateReviewCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockFilmRepository = new Mock<IFilmRepository>();
            _mockReviewRepository = new Mock<IReviewRepository>();
            _mockUserManager = new Mock<UserManager<User>>(
               Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _mockMapper = new Mock<IMapper>();

            _mockRepositoryManager.Setup(x => x.FilmRepository).Returns(_mockFilmRepository.Object);
            _mockRepositoryManager.Setup(x => x.ReviewRepository).Returns(_mockReviewRepository.Object);

            _handler = new CreateReviewCommandHandler(_mockUserManager.Object, _mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_CreatesReview()
        {
            // Arrange
            var filmId = 1;
            var userId = "id";
            var reviewForCreating = new ReviewForCreatingDto
            {
                Stars = 5,
                UserId = userId,
                Comment = "Great movie"
            };

            var film = new FilmEntity { Id = filmId, Title = "New Film", Duration = 120, ReleaseYear = 1999 };
            var user = new User { Id = userId, UserName = "name" };
            var reviewEntity = new ReviewEntity { Id = 1, FilmId = filmId, UserId = userId, Stars = 5, Comment = "Great movie" };
            var reviewDto = new ReviewDto { Id = 1, Stars = 5, Comment = "Great movie" };

            var command = new CreateReviewCommand(filmId, reviewForCreating);

            _mockFilmRepository.Setup(x => x.GetFilmByIdAsync(filmId, false, It.IsAny<CancellationToken>())).ReturnsAsync(film);
            _mockFilmRepository.Setup(x => x.GetFilmByIdAsync(filmId, true, It.IsAny<CancellationToken>())).ReturnsAsync(film);
            _mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockMapper.Setup(x => x.Map<ReviewEntity>(reviewForCreating)).Returns(reviewEntity);
            _mockMapper.Setup(x => x.Map<ReviewDto>(reviewEntity)).Returns(reviewDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockFilmRepository.Verify(x => x.GetFilmByIdAsync(filmId, false, It.IsAny<CancellationToken>()), Times.Once);
            _mockFilmRepository.Verify(x => x.GetFilmByIdAsync(filmId, true, It.IsAny<CancellationToken>()), Times.Once);
            _mockUserManager.Verify(x => x.FindByIdAsync(userId), Times.Once);
            _mockReviewRepository.Verify(x => x.CreateReview(It.IsAny<ReviewEntity>()), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(reviewDto, result);
        }
    }
}
