using FilmoSearchPortal.Application.DTO.Actor;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Actor
{
    public record CreateActorCommand(ActorForCreatingDto ActorForCreating) : IRequest<ActorDto>;
}
