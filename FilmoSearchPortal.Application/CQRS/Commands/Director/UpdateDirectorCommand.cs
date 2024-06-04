using FilmoSearchPortal.Application.DTO.Director;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Director
{
    public record UpdateDirectorCommand(int DirectorId, DirectorForUpdateDto DirectorForUpdateDto, bool TrackChanges) : IRequest<Unit>;
}
