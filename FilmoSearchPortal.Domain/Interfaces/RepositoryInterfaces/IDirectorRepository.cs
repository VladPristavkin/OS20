using FilmoSearchPortal.Domain.Models;

namespace FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces
{
    public interface IDirectorRepository : IRepositoryBase<Director>
    {
        public Task<IEnumerable<Director>> GetAllDirectorsAsync(bool trackChanges, CancellationToken token = default);
        public Task<Director?> GetDirectorByIdAsync(int id, bool trackChanges, CancellationToken token = default);
        public void CreateDirector(Director director);
        public void UpdateDirector(Director director);
        public void DeleteDirector(Director director);
    }
}
