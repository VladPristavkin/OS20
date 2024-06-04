using AutoMapper;
using FilmoSearchPortal.Application.DTO.Review;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Review
{
    internal sealed class GetReviewDetailsHandler : IRequestHandler<GetReviewDetailsQuery, ReviewDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetReviewDetailsHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(GetReviewDetailsQuery request, CancellationToken cancellationToken)
        {
            _ = await _repositoryManager.FilmRepository
                .GetFilmByIdAsync(request.FilmId, request.TrackChanges, cancellationToken) ??
                throw new FilmNotFoundException(request.FilmId);

            var review = await _repositoryManager.ReviewRepository
                .GetReviewByIdAsync(request.ReviewId, request.TrackChanges, cancellationToken) ??
                throw new ReviewNotFoundException(request.ReviewId);

            var reviewDto = _mapper.Map<ReviewDto>(review);

            return reviewDto;
        }
    }
}
