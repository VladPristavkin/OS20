using AutoMapper;
using FilmoSearchPortal.Application.DTO.Genre;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;
using GenreEntity = FilmoSearchPortal.Domain.Models.Genre;

namespace FilmoSearchPortal.Application.CQRS.Commands.Genre
{
    public sealed class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, GenreDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<GenreEntity>(request.GenreForCreating);

            _repositoryManager.GenreRepository.CreateGenre(entity);

            await _repositoryManager.SaveAsync();

            var genreToReturn = _mapper.Map<GenreDto>(entity);

            return genreToReturn;
        }
    }
}
