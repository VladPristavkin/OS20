using FilmoSearchPortal.Application.CQRS.Commands.Director;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using DirectorEntity = FilmoSearchPortal.Domain.Models.Director;

namespace FilmoSearchPortal.Application.Tests.CQRS.Commands.Director
{
    public class DeleteDirectorCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IDirectorRepository> _mockDirectorRepository;
        private readonly DeleteDirectorCommandHandler _handler;

        public DeleteDirectorCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockDirectorRepository = new Mock<IDirectorRepository>();
            _mockRepositoryManager.Setup(x => x.DirectorRepository).Returns(_mockDirectorRepository.Object);

            _handler = new DeleteDirectorCommandHandler(_mockRepositoryManager.Object);
        }

        [Fact]
        public async Task Handle_DeletesDirector()
        {
            // Arrange
            var directorId = 1;
            var director = new DirectorEntity { Id = directorId, Name = "Robert" };
            _mockDirectorRepository.Setup(x => x.GetDirectorByIdAsync(directorId, false, default))
                .ReturnsAsync(director);

            // Act
            await _handler.Handle(new DeleteDirectorCommand(directorId, false), default);

            // Assert
            _mockDirectorRepository.Verify(x => x.DeleteDirector(director), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }

    }
}