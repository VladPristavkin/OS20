using AutoMapper;
using FilmoSearchPortal.Application.DTO.Film;
using FilmoSearchPortal.Domain.Exceptions;
using FilmoSearchPortal.Domain.Interfaces;
using MediatR;
using FilmEntity = FilmoSearchPortal.Domain.Models.Film;
using ActorEntity = FilmoSearchPortal.Domain.Models.Actor;
using GenreEntity = FilmoSearchPortal.Domain.Models.Genre;
using ReviewEntity = FilmoSearchPortal.Domain.Models.Review;

namespace FilmoSearchPortal.Application.CQRS.Commands.Film
{
    public sealed class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, FilmDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateFilmCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<FilmDto> Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FilmEntity>(request.FilmForCreating);

            entity.Rating = 0.0f;

            entity.Director = await _repositoryManager.DirectorRepository
                .GetDirectorByIdAsync(request.FilmForCreating.DirectorId, true, cancellationToken) ??
                throw new DirectorNotFoundException(entity.DirectorId);

            entity.Reviews = new List<ReviewEntity>();

            await HandleActorsAsync(entity, request, cancellationToken);
            await HandleGenresAsync(entity, request, cancellationToken);

            _repositoryManager.FilmRepository.CreateFilm(entity);

            await _repositoryManager.SaveAsync();

            var filmToReturn = _mapper.Map<FilmDto>(entity);

            return filmToReturn;
        }

        private async Task<FilmEntity> HandleActorsAsync(FilmEntity entity, CreateFilmCommand request, CancellationToken cancellationToken)
        {
            if (request.FilmForCreating.ActorIds != null)
            {
                var Actors = new List<ActorEntity>();

                foreach (var id in request.FilmForCreating.ActorIds)
                {
                    var actor = await _repositoryManager.ActorRepository
                        .GetActorByIdAsync(id, true, cancellationToken) ??
                        throw new ActorNotFoundException(id);

                    Actors.Add(actor);
                }

                entity.Actors = Actors;
            }

            return entity;
        }

        private async Task<FilmEntity> HandleGenresAsync(FilmEntity entity, CreateFilmCommand request, CancellationToken cancellationToken)
        {
            if (request.FilmForCreating.GenresIds != null)
            {
                var Genres = new List<GenreEntity>();

                foreach (var id in request.FilmForCreating.GenresIds)
                {
                    var genre = await _repositoryManager.GenreRepository
                        .GetGenreByIdAsync(id, true, cancellationToken) ??
                        throw new GenreNotFoundException(id);

                    Genres.Add(genre);
                }

                entity.Genres = Genres;
            }

            return entity;
        }
    }
}
