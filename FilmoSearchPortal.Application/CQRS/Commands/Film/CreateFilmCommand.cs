using FilmoSearchPortal.Application.DTO.Film;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Film
{
    public record CreateFilmCommand(FilmForCreatingDto FilmForCreating) : IRequest<FilmDto>;
}
