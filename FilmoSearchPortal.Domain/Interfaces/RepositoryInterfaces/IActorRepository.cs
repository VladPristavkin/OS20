using FilmoSearchPortal.Domain.Models;

namespace FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces
{
    public interface IActorRepository : IRepositoryBase<Actor>
    {
        public Task<IEnumerable<Actor>> GetAllActorsAsync(bool trackChanges, CancellationToken token = default);
        public Task<Actor?> GetActorByIdAsync(int id, bool trackChanges, CancellationToken token = default);
        public void DeleteActor(Actor actor);
        public void UpdateActor(Actor actor);
        public void CreateActor(Actor actor);
    }
}
