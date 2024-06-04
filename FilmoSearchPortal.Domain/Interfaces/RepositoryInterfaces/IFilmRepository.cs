using FilmoSearchPortal.Domain.Models;

namespace FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces
{
    public interface IFilmRepository : IRepositoryBase<Film>
    {
        public Task<IEnumerable<Film>> GetAllFilmsAsync(bool trackChanges, CancellationToken token = default);
        public Task<Film?> GetFilmByIdAsync(int id, bool trackChanges, CancellationToken token = default);
        public void CreateFilm(Film film);
        public void UpdateFilm(Film film);
        public void DeleteFilm(Film film);
    }
}
