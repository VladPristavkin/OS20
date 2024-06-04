using FilmoSearchPortal.Application.DTO.Film;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Film
{
    public record UpdateFilmCommand(int FilmId, FilmForUpdateDto FilmForUpdate, bool TrackChanges) : IRequest<Unit>;
}
