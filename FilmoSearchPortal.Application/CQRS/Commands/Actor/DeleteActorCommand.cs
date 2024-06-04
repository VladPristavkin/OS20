using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Actor
{
    public record DeleteActorCommand(int ActorId, bool TrackChanges) : IRequest<Unit>;
}
