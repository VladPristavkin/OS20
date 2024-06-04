using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Genre;
using FilmoSearchPortal.Application.DTO.Genre;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using MediatR;
using Moq;
using Xunit;
using GenreEntity = FilmoSearchPortal.Domain.Models.Genre;

namespace FilmoSearchPortal.UnitTests.Commands.Genre
{
    public class UpdateGenreCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IGenreRepository> _mockGenreRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateGenreCommandHandler _handler;

        public UpdateGenreCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockGenreRepository = new Mock<IGenreRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(_mockGenreRepository.Object);

            _handler = new UpdateGenreCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_UpdatesGenre()
        {
            // Arrange
            var genreId = 1;
            var genreForUpdate = new GenreForUpdateDto { Name = "Comedy" };
            var genreEntity = new GenreEntity { Id = genreId, Name = "Horror" };

            var command = new UpdateGenreCommand(genreId, genreForUpdate, false);

            _mockGenreRepository.Setup(x => x.GetGenreByIdAsync(genreId, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(genreEntity);
            _mockMapper.Setup(x => x.Map(genreForUpdate, genreEntity));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockGenreRepository.Verify(x => x.GetGenreByIdAsync(genreId, false, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(x => x.Map(genreForUpdate, genreEntity), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(Unit.Value, result);
        }
    }
}
