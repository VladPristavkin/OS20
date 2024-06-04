using AutoMapper;
using FilmoSearchPortal.Application.DTO.Review;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Review
{
    internal sealed class GetReviewCollectionHandler : IRequestHandler<GetReviewCollectionQuery, IEnumerable<ReviewDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetReviewCollectionHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> Handle(GetReviewCollectionQuery request, CancellationToken cancellationToken)
        {
            _ = await _repositoryManager.FilmRepository
                 .GetFilmByIdAsync(request.FilmId, request.TrackChanges, cancellationToken) ??
                 throw new FilmNotFoundException(request.FilmId);

            var requests = await _repositoryManager.ReviewRepository
                .GetAllReviewsAsync(request.FilmId, request.TrackChanges, cancellationToken);

            var requestsDto = _mapper.Map<IEnumerable<ReviewDto>>(requests);

            return requestsDto;
        }
    }
}
