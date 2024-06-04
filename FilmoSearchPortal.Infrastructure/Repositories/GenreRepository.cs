using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Models;
using FilmoSearchPortal.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.Infrastructure.Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public void CreateGenre(Genre genre) => Create(genre);

        public void DeleteGenre(Genre genre) => Delete(genre);

        public async Task<IEnumerable<Genre>> GetAllGenresAsync(bool trackChanges, CancellationToken token = default) =>
            await FindAll(trackChanges)
            .Include(gn => gn.Films)
            .ToListAsync(token);

        public async Task<Genre?> GetGenreByIdAsync(int id, bool trackChanges, CancellationToken token = default) =>
            await FindAllByExpression(gn => gn.Id == id, trackChanges)
            .Include(gn => gn.Films)
            .SingleOrDefaultAsync(token);

        public void UpdateGenre(Genre genre) => Update(genre);
    }
}
