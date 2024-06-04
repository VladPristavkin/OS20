using FilmoSearchPortal.Application.DTO.Genre;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Genre
{
    public record GetGenreCollectionQuery(bool TrackChanges) : IRequest<IEnumerable<GenreDto>>;
}
