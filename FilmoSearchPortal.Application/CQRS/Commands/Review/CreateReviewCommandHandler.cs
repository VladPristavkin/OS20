using AutoMapper;
using FilmoSearchPortal.Application.DTO.Review;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ReviewEntity = FilmoSearchPortal.Domain.Models.Review;
using UserEntity = FilmoSearchPortal.Domain.Models.User;

namespace FilmoSearchPortal.Application.CQRS.Commands.Review
{
    public sealed class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(UserManager<UserEntity> userManager,IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            _ = await _repositoryManager.FilmRepository
                .GetFilmByIdAsync(request.FilmId, trackChanges: false, cancellationToken) ??
                throw new FilmNotFoundException(request.FilmId);

            var entity = _mapper.Map<ReviewEntity>(request.ReviewForCreating);

            entity.FilmId = request.FilmId;

            entity.Film = await _repositoryManager.FilmRepository
                .GetFilmByIdAsync(entity.FilmId, true, cancellationToken)
                ?? throw new FilmNotFoundException(entity.FilmId);

            entity.User = await _userManager.FindByIdAsync(request.ReviewForCreating.UserId);

            entity.Date = DateTime.UtcNow;

            _repositoryManager.ReviewRepository.CreateReview(entity);

            await _repositoryManager.SaveAsync();

            var reviewToReturn = _mapper.Map<ReviewDto>(entity);

            return reviewToReturn;
        }
    }
}
