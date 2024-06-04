using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Film
{
    public sealed class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DeleteFilmCommandHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Unit> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            var film = await _repositoryManager.FilmRepository
                .GetFilmByIdAsync(request.FilmId, request.TrackChanges, cancellationToken) ??
                throw new FilmNotFoundException(request.FilmId);

            _repositoryManager.FilmRepository.DeleteFilm(film);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
