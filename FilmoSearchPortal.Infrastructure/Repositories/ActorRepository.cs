using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Models;
using FilmoSearchPortal.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.Infrastructure.Repositories
{
    internal class ActorRepository : RepositoryBase<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public void CreateActor(Actor actor) => Create(actor);

        public void DeleteActor(Actor actor) => Delete(actor);

        public async Task<Actor?> GetActorByIdAsync(int id, bool trackChanges, CancellationToken token = default) =>
            await FindAllByExpression(ac => ac.Id == id, trackChanges)
            .Include(ac => ac.Films)
            .SingleOrDefaultAsync(token);

        public async Task<IEnumerable<Actor>> GetAllActorsAsync(bool trackChanges, CancellationToken token = default) =>
            await FindAll(trackChanges)
            .Include(ac => ac.Films)
            .ToListAsync(token);

        public void UpdateActor(Actor actor) => Update(actor);
    }
}
