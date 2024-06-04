namespace FilmoSearchPortal.Domain.Exceptions
{
    public class DirectorNotFoundException : NotFoundException
    {
        public DirectorNotFoundException(int directorId) : base($"Director with id:{directorId} not found.") { }
    }
}
