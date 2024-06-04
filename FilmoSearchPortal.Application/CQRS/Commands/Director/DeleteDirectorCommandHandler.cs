using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Director
{
    public sealed class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DeleteDirectorCommandHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Unit> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _repositoryManager.DirectorRepository
                .GetDirectorByIdAsync(request.DirectorId, request.TrackChanges, cancellationToken) ??
                throw new DirectorNotFoundException(request.DirectorId);

            _repositoryManager.DirectorRepository.DeleteDirector(director);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
