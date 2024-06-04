using FilmoSearchPortal.Application.CQRS.Commands.Actor;
using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using Moq;
using Xunit;
using ActorEntity = FilmoSearchPortal.Domain.Models.Actor;

namespace FilmoSearchPortal.UnitTests.Commands.Actor
{
    public class DeleteActorCommandHandlerTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IActorRepository> _mockActorRepository;
        private readonly DeleteActorCommandHandler _handler;

        public DeleteActorCommandHandlerTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockActorRepository = new Mock<IActorRepository>();
            _mockRepositoryManager.Setup(x => x.ActorRepository).Returns(_mockActorRepository.Object);

            _handler = new DeleteActorCommandHandler(_mockRepositoryManager.Object);
        }

        [Fact]
        public async Task Handle_DeletesActor()
        {
            // Arrange
            var actorId = 1;
            var actor = new ActorEntity { Id = actorId, Name = "Robert" };
            _mockActorRepository.Setup(x => x.GetActorByIdAsync(actorId, false, default))
                .ReturnsAsync(actor);

            // Act
            await _handler.Handle(new DeleteActorCommand(actorId, false), default);

            // Assert
            _mockActorRepository.Verify(x => x.DeleteActor(actor), Times.Once);
            _mockRepositoryManager.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
