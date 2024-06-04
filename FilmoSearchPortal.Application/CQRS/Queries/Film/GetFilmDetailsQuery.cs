using FilmoSearchPortal.Application.DTO.Film;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Film
{
    public record GetFilmDetailsQuery(int FilmId, bool TrackChanges) : IRequest<FilmDto>;
}
