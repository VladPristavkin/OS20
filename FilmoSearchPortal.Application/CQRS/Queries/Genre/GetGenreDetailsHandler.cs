using AutoMapper;
using FilmoSearchPortal.Application.DTO.Genre;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Genre
{
    internal sealed class GetGenreDetailsHandler : IRequestHandler<GetGenreDetailsQuery, GenreDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetGenreDetailsHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(GetGenreDetailsQuery request, CancellationToken cancellationToken)
        {
            var genre = await _repositoryManager.GenreRepository
                .GetGenreByIdAsync(request.GenreId, request.TrackChanges, cancellationToken) ??
                throw new GenreNotFoundException(request.GenreId);

            var genreDto = _mapper.Map<GenreDto>(genre);

            return genreDto;
        }
    }
}
