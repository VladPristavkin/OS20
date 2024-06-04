using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Genre
{
    public sealed class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DeleteGenreCommandHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _repositoryManager.GenreRepository
                .GetGenreByIdAsync(request.GenreId, request.TrackChanges) ??
                throw new GenreNotFoundException(request.GenreId);

            _repositoryManager.GenreRepository.DeleteGenre(genre);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
