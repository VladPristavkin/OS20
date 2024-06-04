using AutoMapper;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Film
{
    public sealed class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateFilmCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
        {
            var film = await _repositoryManager.FilmRepository
                .GetFilmByIdAsync(request.FilmId, request.TrackChanges, cancellationToken) ??
                throw new FilmNotFoundException(request.FilmId);

            _mapper.Map(request.FilmForUpdate, film);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
