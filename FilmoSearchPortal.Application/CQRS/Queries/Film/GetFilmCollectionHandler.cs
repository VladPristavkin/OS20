using AutoMapper;
using FilmoSearchPortal.Application.DTO.Film;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Film
{
    internal sealed class GetFilmCollectionHandler : IRequestHandler<GetFilmCollectionQuery, IEnumerable<FilmDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetFilmCollectionHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<FilmDto>> Handle(GetFilmCollectionQuery request, CancellationToken cancellationToken)
        {
            var films = await _repositoryManager.FilmRepository.GetAllFilmsAsync(request.TrackChanges, cancellationToken);

            var filmsDto = _mapper.Map<IEnumerable<FilmDto>>(films);

            return filmsDto;
        }
    }
}
