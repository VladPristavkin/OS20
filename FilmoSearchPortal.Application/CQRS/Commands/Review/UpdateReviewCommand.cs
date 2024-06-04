using FilmoSearchPortal.Application.DTO.Review;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Review
{
    public record UpdateReviewCommand(int ReviewId, ReviewForUpdateDto ReviewForUpdate, bool TrackChanges) : IRequest<Unit>;
}
