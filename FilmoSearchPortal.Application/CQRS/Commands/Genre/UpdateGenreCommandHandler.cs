using AutoMapper;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.Genre
{
    public sealed class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Unit>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryManager.GenreRepository
                .GetGenreByIdAsync(request.GenreId, request.TrackChanges, cancellationToken) ??
                throw new GenreNotFoundException(request.GenreId);

            _mapper.Map(request.GenreForUpdate, entity);

            await _repositoryManager.SaveAsync();

            return Unit.Value;
        }
    }
}
