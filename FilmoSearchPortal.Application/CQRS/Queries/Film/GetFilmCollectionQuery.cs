using FilmoSearchPortal.Application.DTO.Film;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Film
{
    public record GetFilmCollectionQuery(bool TrackChanges) : IRequest<IEnumerable<FilmDto>>;
}