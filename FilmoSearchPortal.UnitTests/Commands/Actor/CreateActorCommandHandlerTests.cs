using Xunit;
using Moq;
using FilmoSearchPortal.Domain.Interfaces;
using AutoMapper;
using FilmoSearchPortal.Application.CQRS.Commands.Actor;
using FilmoSearchPortal.Application.DTO.Actor;
using ActorEntity = FilmoSearchPortal.Domain.Models.Actor;

namespace FilmoSearchPortal.UnitTests.Commands.Actor
{
    public class CreateActorCommandHandlerTests
    {
        [Fact]
        public async Task Handle_CreateActor()
        {
            // Arrange
            var actorForCreatingDto = new ActorForCreatingDto
            {
                Name = "Robert",
                Biography = "Some biography"
            };

            var actorEntity = new ActorEntity
            {
                Id = 1,
                Name = "Robert",
                Biography = "Some biography"
            };

            var actorDto = new ActorDto
            {
                Id = 1,
                Name = "Robert",
                Biography = "Some biography"
            };

            var mockRepositoryManager = new Mock<IRepositoryManager>();
            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(m => m.Map<ActorEntity>(It.IsAny<ActorForCreatingDto>()))
                .Returns(actorEntity);
            mockMapper.Setup(m => m.Map<ActorDto>(It.IsAny<ActorEntity>()))
                .Returns(actorDto);

            mockRepositoryManager.Setup(r => r.ActorRepository.CreateActor(It.IsAny<ActorEntity>()));
            mockRepositoryManager.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var handler = new CreateActorCommandHandler(mockRepositoryManager.Object, mockMapper.Object);
            var command = new CreateActorCommand(actorForCreatingDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(actorDto, result);
            mockRepositoryManager.Verify(r => r.ActorRepository.CreateActor(It.Is<ActorEntity>(a =>
                a.Name == actorForCreatingDto.Name && a.Biography == actorForCreatingDto.Biography
            )), Times.Once);
            mockRepositoryManager.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}
