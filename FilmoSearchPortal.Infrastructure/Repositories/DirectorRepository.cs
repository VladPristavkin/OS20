using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Models;
using FilmoSearchPortal.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.Infrastructure.Repositories
{
    public class DirectorRepository : RepositoryBase<Director>, IDirectorRepository
    {
        public DirectorRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public void CreateDirector(Director director) => Create(director);

        public void DeleteDirector(Director director) => Delete(director);

        public async Task<IEnumerable<Director>> GetAllDirectorsAsync(bool trackChanges,
            CancellationToken token = default) =>
            await FindAll(trackChanges)
            .Include(dr => dr.Films)
            .ToListAsync(token);

        public async Task<Director?> GetDirectorByIdAsync(int id, bool trackChanges,
            CancellationToken token = default) =>
            await FindAllByExpression(dr => dr.Id == id, trackChanges)
            .SingleOrDefaultAsync(token);

        public void UpdateDirector(Director director) => Update(director);
    }
}
