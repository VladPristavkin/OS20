using FilmoSearchPortal.Application.DTO.Genre;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Genre
{
    public record CreateGenreCommand(GenreForCreatingDto GenreForCreating) : IRequest<GenreDto>;
}
