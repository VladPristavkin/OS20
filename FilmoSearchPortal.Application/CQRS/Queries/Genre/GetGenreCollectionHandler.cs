using AutoMapper;
using FilmoSearchPortal.Application.DTO.Genre;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Genre
{
    internal sealed class GetGenreCollectionHandler : IRequestHandler<GetGenreCollectionQuery, IEnumerable<GenreDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetGenreCollectionHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetGenreCollectionQuery request, CancellationToken cancellationToken)
        {
            var genres = await _repositoryManager.GenreRepository
                .GetAllGenresAsync(request.TrackChanges, cancellationToken);

            var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genres);

            return genresDto;
        }
    }
}
