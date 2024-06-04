namespace FilmoSearchPortal.Domain.Exceptions
{
    public class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(int genreId) : base($"Genre with id:{genreId} not found.") { }
    }
}
