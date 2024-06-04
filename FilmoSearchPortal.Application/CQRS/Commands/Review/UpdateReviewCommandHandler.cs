using AutoMapper;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Review
{
    public sealed class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryManager.ReviewRepository
                .GetReviewByIdAsync(request.ReviewId, request.TrackChanges, cancellationToken) ??
                throw new ReviewNotFoundException(request.ReviewId);

            _mapper.Map(request.ReviewForUpdate, entity);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
