using FilmoSearchPortal.Domain.Models;

namespace FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces
{
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
        public Task<IEnumerable<Genre>> GetAllGenresAsync(bool trackChanges, CancellationToken token = default);
        public Task<Genre?> GetGenreByIdAsync(int id, bool trackChanges, CancellationToken token = default);
        public void CreateGenre(Genre genre);
        public void UpdateGenre(Genre genre);
        public void DeleteGenre(Genre genre);
    }
}
