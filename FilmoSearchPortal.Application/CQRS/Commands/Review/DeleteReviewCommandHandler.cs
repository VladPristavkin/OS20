using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Review
{
    public sealed class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DeleteReviewCommandHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repositoryManager.ReviewRepository
                .GetReviewByIdAsync(request.ReviewId, request.TrackChanges, cancellationToken) ??
                throw new ReviewNotFoundException(request.ReviewId);

            _repositoryManager.ReviewRepository.DeleteReview(review);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
