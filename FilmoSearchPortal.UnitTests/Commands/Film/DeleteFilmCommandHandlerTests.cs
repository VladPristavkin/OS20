using FilmoSearchPortal.Application.CQRS.Commands.Film;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using FilmEntity = FilmoSearchPortal.Domain.Models.Film;

namespace FilmoSearchPortal.UnitTests.Commands.Film
{
    public class DeleteFilmCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IFilmRepository> _mockFilmRepository;
        private readonly DeleteFilmCommandHandler _handler;

        public DeleteFilmCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockFilmRepository = new Mock<IFilmRepository>();
            _mockRepositoryManager.Setup(x => x.FilmRepository).Returns(_mockFilmRepository.Object);

            _handler = new DeleteFilmCommandHandler(_mockRepositoryManager.Object);
        }

        [Fact]
        public async Task Handle_DeletesFilm()
        {
            // Arrange
            var filmId = 1;
            var film = new FilmEntity { Id = filmId, Title = "New Film", Duration = 120, ReleaseYear = 1999 };
            _mockFilmRepository.Setup(x => x.GetFilmByIdAsync(filmId, false, default))
                .ReturnsAsync(film);

            // Act
            await _handler.Handle(new DeleteFilmCommand(filmId, false), default);

            // Assert
            _mockFilmRepository.Verify(x => x.DeleteFilm(film), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
