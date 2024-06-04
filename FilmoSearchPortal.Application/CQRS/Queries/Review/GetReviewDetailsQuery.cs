using FilmoSearchPortal.Application.DTO.Review;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Review
{
    public record GetReviewDetailsQuery(int FilmId, int ReviewId, bool TrackChanges) : IRequest<ReviewDto>;
}
