namespace FilmoSearchPortal.Domain.Exceptions
{
    public class ReviewNotFoundException : NotFoundException
    {
        public ReviewNotFoundException(int reviewId) : base($"Review with id:{reviewId} not found.") { }
    }
}
