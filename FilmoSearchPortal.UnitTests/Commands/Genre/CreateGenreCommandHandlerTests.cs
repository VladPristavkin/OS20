using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Genre;
using FilmoSearchPortal.Application.DTO.Genre;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Interfaces;
using Moq;
using Xunit;
using GenreEntity = FilmoSearchPortal.Domain.Models.Genre;

namespace FilmoSearchPortal.Application.Tests.CQRS.Commands.Genre
{
    public class CreateGenreCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IGenreRepository> _mockGenreRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateGenreCommandHandler _handler;

        public CreateGenreCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockGenreRepository = new Mock<IGenreRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(_mockGenreRepository.Object);

            _handler = new CreateGenreCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_CreateNewGenre()
        {
            // Arrange
            var genreForCreating = new GenreForCreatingDto { Name = "Horror" };
            var genre = new GenreEntity { Id = 1, Name = genreForCreating.Name };
            var genreDto = new GenreDto { Id = 2, Name = genre.Name };

            _mockMapper.Setup(x => x.Map<GenreEntity>(genreForCreating)).Returns(genre);
            _mockMapper.Setup(x => x.Map<GenreDto>(genre)).Returns(genreDto);

            // Act
            var result = await _handler.Handle(new CreateGenreCommand(genreForCreating), default);

            // Assert
            _mockGenreRepository.Verify(x => x.CreateGenre(genre), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(genreDto, result);
        }
    }
}