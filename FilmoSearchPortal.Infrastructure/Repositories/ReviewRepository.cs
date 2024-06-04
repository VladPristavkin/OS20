using FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces;
using FilmoSearchPortal.Domain.Models;
using FilmoSearchPortal.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearchPortal.Infrastructure.Repositories
{
    internal class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public void CreateReview(Review review) => Create(review);

        public void DeleteReview(Review review) => Delete(review);

        public async Task<IEnumerable<Review>> GetAllReviewsAsync(int filmId, bool trackChanges,
            CancellationToken token = default) =>
            await FindAllByExpression(rv => rv.FilmId == filmId, trackChanges)
            .Include(rv => rv.User)
            .ToListAsync(token);

        public async Task<Review?> GetReviewByIdAsync(int id, bool trackChanges,
            CancellationToken token = default) =>
            await FindAllByExpression(rv => rv.Id == id, trackChanges)
            .Include(rv => rv.User)
            .SingleOrDefaultAsync(token);

        public void UpdateReview(Review review) => Update(review);
    }
}
