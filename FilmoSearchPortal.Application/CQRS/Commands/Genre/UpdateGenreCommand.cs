using FilmoSearchPortal.Application.DTO.Genre;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Genre
{
    public record UpdateGenreCommand(int GenreId, GenreForUpdateDto GenreForUpdate, bool TrackChanges) : IRequest<Unit>;
}
