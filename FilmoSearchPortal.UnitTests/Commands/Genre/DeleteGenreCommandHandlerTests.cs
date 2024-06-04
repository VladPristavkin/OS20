using FilmoSearchPortal.Application.CQRS.Commands.Genre;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using GenreEntity = FilmoSearchPortal.Domain.Models.Genre;

namespace FilmoSearchPortal.UnitTests.Commands.Genre
{
    public class DeleteGenreCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IGenreRepository> _mockGenreRepository;
        private readonly DeleteGenreCommandHandler _handler;

        public DeleteGenreCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockGenreRepository = new Mock<IGenreRepository>();
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(_mockGenreRepository.Object);

            _handler = new DeleteGenreCommandHandler(_mockRepositoryManager.Object);
        }

        [Fact]
        public async Task Handle_DeletesGenre()
        {
            // Arrange
            var genreId = 1;
            var genre = new GenreEntity { Id = genreId, Name = "Horror" };
            _mockGenreRepository.Setup(x => x.GetGenreByIdAsync(genreId, false, default)).ReturnsAsync(genre);

            // Act
            await _handler.Handle(new DeleteGenreCommand(genreId, false), default);

            // Assert
            _mockGenreRepository.Verify(x => x.DeleteGenre(genre), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
