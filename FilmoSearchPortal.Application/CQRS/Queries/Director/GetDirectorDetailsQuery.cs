using FilmoSearchPortal.Application.DTO.Director;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Director
{
    public record GetDirectorDetailsQuery(int DirectorId, bool TrackChanges) : IRequest<DirectorDto>;
}
