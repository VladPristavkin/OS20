using FilmoSearchPortal.Application.DTO.Review;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Queries.Review
{
    public record GetReviewCollectionQuery(int FilmId, bool TrackChanges) : IRequest<IEnumerable<ReviewDto>>;
}
