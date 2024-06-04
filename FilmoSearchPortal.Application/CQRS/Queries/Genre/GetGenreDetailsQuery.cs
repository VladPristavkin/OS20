using FilmoSearchPortal.Application.DTO.Genre;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Genre
{
    public record GetGenreDetailsQuery(int GenreId, bool TrackChanges) : IRequest<GenreDto>;
}
