using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Actor;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Interfaces;
using Moq;
using Xunit;
using ActorEntity = FilmoSearchPortal.Domain.Models.Actor;
using FilmoSearchPortal.Application.DTO.Actor;

namespace FilmoSearchPortal.UnitTests.Commands.Actor
{
    public class UpdateActorCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IActorRepository> _mockActorRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateActorCommandHandler _handler;

        public UpdateActorCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockActorRepository = new Mock<IActorRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRepositoryManager.Setup(x => x.ActorRepository).Returns(_mockActorRepository.Object);

            _handler = new UpdateActorCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_UpdatesActor()
        {
            // Arrange
            var actorId = 1;
            var actor = new ActorEntity { Id = actorId, Name = "Robert" };
            var actorForUpdate = new ActorForUpdateDto { Name = "Updated Name" };
            _mockActorRepository.Setup(x => x.GetActorByIdAsync(actorId, false, default))
                .ReturnsAsync(actor);

            // Act
            await _handler.Handle(new UpdateActorCommand(actorId, actorForUpdate, false), default);

            // Assert
            _mockMapper.Verify(x => x.Map(actorForUpdate, actor), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
