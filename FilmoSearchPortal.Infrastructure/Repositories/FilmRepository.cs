using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Models;
using FilmoSearchPortal.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.Infrastructure.Repositories
{
    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public void CreateFilm(Film film) => Create(film);

        public void DeleteFilm(Film film) => Delete(film);

        public async Task<IEnumerable<Film>> GetAllFilmsAsync(bool trackChanges, CancellationToken token = default) =>
            await FindAll(trackChanges)
            .Include(fl => fl.Genres)
            .Include(fl => fl.Actors)
            .Include(fl => fl.Director)
            .Include(fl => fl.Reviews)
            .ThenInclude(rv => rv.User)
            .ToListAsync(token);

        public async Task<Film?> GetFilmByIdAsync(int id, bool trackChanges, CancellationToken token = default) =>
            await FindAllByExpression(fl => fl.Id == id, trackChanges)
            .Include(fl => fl.Genres)
            .Include(fl => fl.Actors)
            .Include(fl => fl.Director)
            .Include(fl => fl.Reviews)
            .ThenInclude(rv => rv.User)
            .SingleOrDefaultAsync(token);

        public void UpdateFilm(Film film) => Update(film);
    }
}
