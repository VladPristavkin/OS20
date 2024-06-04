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
    public class CreateDirectorCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IDirectorRepository> _mockDirectorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateDirectorCommandHandler _handler;

        public CreateDirectorCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockDirectorRepository = new Mock<IDirectorRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryManager.Setup(x => x.DirectorRepository).Returns(_mockDirectorRepository.Object);

            _handler = new CreateDirectorCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_CreateNewDirector()
        {
            // Arrange
            var directorId = 1;
            var directorForCreating = new DirectorForCreatingDto { Name = "Robert" };
            var director = new DirectorEntity { Id = directorId, Name = directorForCreating.Name };
            var directorDto = new DirectorDto { Id = directorId, Name = director.Name };

            _mockMapper.Setup(x => x.Map<DirectorEntity>(directorForCreating)).Returns(director);
            _mockMapper.Setup(x => x.Map<DirectorDto>(director)).Returns(directorDto);

            // Act
            var result = await _handler.Handle(new CreateDirectorCommand(directorForCreating), default);

            // Assert
            _mockDirectorRepository.Verify(x => x.CreateDirector(director), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
            Assert.Equal(directorDto, result);
        }
    }
}
