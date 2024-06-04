using FilmoSearchPortal.Application.DTO.Actor;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Actor
{
    public record GetActorCollectionQuery(bool TrackChanges) : IRequest<IEnumerable<ActorDto>>;
}
