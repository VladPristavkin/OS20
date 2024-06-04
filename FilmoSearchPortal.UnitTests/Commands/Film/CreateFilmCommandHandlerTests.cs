using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Film;
using FilmoSearchPortal.Application.DTO.Film;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using ActorEntity = FilmoSearchPortal.Domain.Models.Actor;
using DirectorEntity = FilmoSearchPortal.Domain.Models.Director;
using FilmEntity = FilmoSearchPortal.Domain.Models.Film;
using GenreEntity = FilmoSearchPortal.Domain.Models.Genre;

namespace FilmoSearchPortal.UnitTests.Commands.Film
{
    public class CreateFilmCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IFilmRepository> _mockFilmRepository;
        private readonly Mock<IDirectorRepository> _mockDirectorRepository;
        private readonly Mock<IActorRepository> _mockActorRepository;
        private readonly Mock<IGenreRepository> _mockGenreRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateFilmCommandHandler _handler;

        public CreateFilmCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockFilmRepository = new Mock<IFilmRepository>();
            _mockDirectorRepository = new Mock<IDirectorRepository>();
            _mockActorRepository = new Mock<IActorRepository>();
            _mockGenreRepository = new Mock<IGenreRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockRepositoryManager.Setup(x => x.FilmRepository).Returns(_mockFilmRepository.Object);
            _mockRepositoryManager.Setup(x => x.DirectorRepository).Returns(_mockDirectorRepository.Object);
            _mockRepositoryManager.Setup(x => x.ActorRepository).Returns(_mockActorRepository.Object);
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(_mockGenreRepository.Object);

            _handler = new CreateFilmCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_CreateNewFilm()
        {
            // Arrange
            var filmForCreating = new FilmForCreatingDto
            {
                Id = 1,
                Duration = 120,
                ReleaseYear = 1999,
                Title = "New Film",
                DirectorId = 1,
                ActorIds = new[] { 1, 2 },
                GenresIds = new[] { 1, 2 }
            };

            var director = new DirectorEntity { Id = filmForCreating.DirectorId, Name = "Robert" };
            var actors = new List<ActorEntity>
            {
                new ActorEntity { Id = 1, Name = "Robert" },
                new ActorEntity { Id = 2, Name = "Robert" }
            };
            var genres = new List<GenreEntity>
            {
                new GenreEntity { Id = 1, Name = "Horror" },
                new GenreEntity { Id = 2, Name = "Comedy" }
            };

            var film = new FilmEntity
            {
                Id = filmForCreating.Id,
                Duration = filmForCreating.Duration,
                ReleaseYear = filmForCreating.ReleaseYear,
                Title = filmForCreating.Title,
                DirectorId = filmForCreating.DirectorId,
                Director = director,
                Actors = actors,
                Genres = genres
            };

            var filmDto = new FilmDto { Id = 1, Duration = 120, ReleaseYear = 1999, Title = film.Title };

            _mockMapper.Setup(x => x.Map<FilmEntity>(filmForCreating)).Returns(film);
            _mockMapper.Setup(x => x.Map<FilmDto>(film)).Returns(filmDto);
            _mockDirectorRepository.Setup(x => x.GetDirectorByIdAsync(filmForCreating.DirectorId, true, default)).ReturnsAsync(director);

            // Добавление моков для актеров и жанров
            foreach (var actor in actors)
            {
                _mockActorRepository.Setup(x => x.GetActorByIdAsync(actor.Id, true, default)).ReturnsAsync(actor);
            }

            foreach (var genre in genres)
            {
                _mockGenreRepository.Setup(x => x.GetGenreByIdAsync(genre.Id, true, default)).ReturnsAsync(genre);
            }

            // Act
            var result = await _handler.Handle(new CreateFilmCommand(filmForCreating), default);

            // Assert
            _mockFilmRepository.Verify(x => x.CreateFilm(It.IsAny<FilmEntity>()), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(filmDto, result);
        }
    }
}
