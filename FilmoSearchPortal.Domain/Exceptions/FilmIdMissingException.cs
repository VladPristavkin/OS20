namespace FilmoSearchPortal.Domain.Exceptions
{
    public class FilmIdMissingException : Exception
    {
        public FilmIdMissingException() : base("FilmId is required and cannot be null.") { }

        public FilmIdMissingException(string message) : base(message) { }
    }
}
