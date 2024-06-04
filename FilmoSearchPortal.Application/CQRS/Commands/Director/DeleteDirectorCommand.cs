using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Director
{
    public record DeleteDirectorCommand(int DirectorId, bool TrackChanges) : IRequest<Unit>;
}
