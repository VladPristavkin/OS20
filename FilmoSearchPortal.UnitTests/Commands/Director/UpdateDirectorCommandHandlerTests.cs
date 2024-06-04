using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Director;
using FilmoSearchPortal.Application.DTO.Director;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using DirectorEntity = FilmoSearchPortal.Domain.Models.Director;

namespace FilmoSearchPortal.UnitTests.Commands.Director
{
    public class UpdateDirectorCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IDirectorRepository> _mockDirectorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateDirectorCommandHandler _handler;

        public UpdateDirectorCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockDirectorRepository = new Mock<IDirectorRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryManager.Setup(x => x.DirectorRepository).Returns(_mockDirectorRepository.Object);

            _handler = new UpdateDirectorCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_UpdatesDirector()
        {
            // Arrange
            var directorId = 1;
            var director = new DirectorEntity { Id = directorId, Name = "Robert" };
            var directorForUpdateDto = new DirectorForUpdateDto { Name = "Updated Name" };
            _mockDirectorRepository.Setup(x => x.GetDirectorByIdAsync(directorId, false, default))
                .ReturnsAsync(director);

            // Act
            await _handler.Handle(new UpdateDirectorCommand(directorId, directorForUpdateDto, false), default);

            // Assert
            _mockMapper.Verify(x => x.Map(directorForUpdateDto, director), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
