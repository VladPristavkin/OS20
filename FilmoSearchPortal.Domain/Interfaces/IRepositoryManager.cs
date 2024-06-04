using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;

namespace FilmoSearchPortal.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IActorRepository ActorRepository { get; }
        public IDirectorRepository DirectorRepository { get; }
        public IFilmRepository FilmRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IReviewRepository ReviewRepository { get; }

        public Task SaveAsync();
    }
}
