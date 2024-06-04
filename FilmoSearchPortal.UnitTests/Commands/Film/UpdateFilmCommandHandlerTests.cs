using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Film;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using FilmEntity = FilmoSearchPortal.Domain.Models.Film;

namespace FilmoSearchPortal.UnitTests.Commands.Film
{
    public class UpdateFilmCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IFilmRepository> _mockFilmRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateFilmCommandHandler _handler;

        public UpdateFilmCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockFilmRepository = new Mock<IFilmRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryManager.Setup(x => x.FilmRepository).Returns(_mockFilmRepository.Object);

            _handler = new UpdateFilmCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_UpdatesFilm()
        {
            // Arrange
            var filmId = 1;
            var film = new FilmEntity { Id = filmId, Title = "Old Film", Duration = 120, ReleaseYear = 1999 };
            var filmForUpdate = new Application.DTO.Film.FilmForUpdateDto { Id = filmId, Title = "New Film", Duration = 100, ReleaseYear = 2000 };
            _mockFilmRepository.Setup(x => x.GetFilmByIdAsync(filmId, false, default))
                .ReturnsAsync(film);

            // Act
            await _handler.Handle(new UpdateFilmCommand(filmId, filmForUpdate, false), default);

            // Assert
            _mockMapper.Verify(x => x.Map(filmForUpdate, film), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
