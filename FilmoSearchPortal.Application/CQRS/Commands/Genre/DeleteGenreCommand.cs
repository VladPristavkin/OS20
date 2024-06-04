using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Genre
{
    public record DeleteGenreCommand(int GenreId, bool TrackChanges) : IRequest<Unit>;
}
