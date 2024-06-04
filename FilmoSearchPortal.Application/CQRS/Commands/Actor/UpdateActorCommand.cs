using FilmoSearchPortal.Application.DTO.Actor;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Actor
{
    public record UpdateActorCommand(int ActorId, ActorForUpdateDto ActorForUpdate, bool TrackChanges) : IRequest<Unit>;
}
