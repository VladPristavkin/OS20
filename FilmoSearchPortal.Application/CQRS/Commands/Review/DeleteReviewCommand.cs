using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Review
{
    public record DeleteReviewCommand(int ReviewId, bool TrackChanges) : IRequest<Unit>;
}
