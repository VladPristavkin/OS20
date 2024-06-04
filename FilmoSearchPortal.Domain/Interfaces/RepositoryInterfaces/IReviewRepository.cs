using FilmoSearchPortal.Domain.Models;

namespace FilmoSearchPortal.Domain.Interfaces.RepositoryInterfaces
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        public Task<IEnumerable<Review>> GetAllReviewsAsync(int filmId, bool trackChanges, CancellationToken token = default);
        public Task<Review?> GetReviewByIdAsync(int id, bool trackChanges, CancellationToken token = default);
        public void CreateReview(Review review);
        public void UpdateReview(Review review);
        public void DeleteReview(Review review);
    }
}
