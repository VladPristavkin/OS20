using FilmoSearchPortal.Application.DTO.Actor;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Actor
{
    public record GetActorDetailsQuery(int ActorId, bool TrackChanges) : IRequest<ActorDto>;
}