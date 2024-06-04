using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Film
{
    public record DeleteFilmCommand(int FilmId, bool TrackChanges) : IRequest<Unit>;
}
