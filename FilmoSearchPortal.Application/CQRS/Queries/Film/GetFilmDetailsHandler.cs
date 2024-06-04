using AutoMapper;
using FilmoSearchPortal.Application.DTO.Film;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Film
{
    internal sealed class GetFilmDetailsHandler : IRequestHandler<GetFilmDetailsQuery, FilmDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetFilmDetailsHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<FilmDto> Handle(GetFilmDetailsQuery request, CancellationToken cancellationToken)
        {
            var film = await _repositoryManager.FilmRepository
                .GetFilmByIdAsync(request.FilmId, request.TrackChanges, cancellationToken) ??
                throw new FilmNotFoundException(request.FilmId);

            var filmDto = _mapper.Map<FilmDto>(film);

            return filmDto;
        }
    }
}
