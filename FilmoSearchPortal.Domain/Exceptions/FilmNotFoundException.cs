namespace FilmoSearchPortal.Domain.Exceptions
{
    public class FilmNotFoundException : NotFoundException
    {
        public FilmNotFoundException(int filmId) : base($"Film with id:{filmId} not found.") { }
    }
}
