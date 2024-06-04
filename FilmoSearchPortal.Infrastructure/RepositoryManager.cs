using FilmoSearchPortal.Domain.Interfaces;
using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Infrastructure.DbContexts;
using FilmoSearchPortal.Infrastructure.Repositories;

namespace FilmoSearchPortal.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Lazy<IActorRepository> _actorRepository;
        private readonly Lazy<IDirectorRepository> _directorRepository;
        private readonly Lazy<IFilmRepository> _filmRepository;
        private readonly Lazy<IGenreRepository> _genreRepository;
        private readonly Lazy<IReviewRepository> _reviewRepository;

        public RepositoryManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _actorRepository = new Lazy<IActorRepository>(() => new ActorRepository(_dbContext));
            _directorRepository = new Lazy<IDirectorRepository>(() => new DirectorRepository(_dbContext));
            _filmRepository = new Lazy<IFilmRepository>(() => new FilmRepository(_dbContext));
            _genreRepository = new Lazy<IGenreRepository>(() => new GenreRepository(_dbContext));
            _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(_dbContext));
        }

        public IActorRepository ActorRepository => _actorRepository.Value;

        public IDirectorRepository DirectorRepository => _directorRepository.Value;

        public IFilmRepository FilmRepository => _filmRepository.Value;

        public IGenreRepository GenreRepository => _genreRepository.Value;

        public IReviewRepository ReviewRepository => _reviewRepository.Value;

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}
