using FilmoSearchPortal.Application.DTO.Director;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Director
{
    public record CreateDirectorCommand(DirectorForCreatingDto DirectorForCreating) : IRequest<DirectorDto>;
}
