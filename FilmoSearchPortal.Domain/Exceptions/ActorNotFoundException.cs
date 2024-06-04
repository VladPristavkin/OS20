namespace FilmoSearchPortal.Domain.Exceptions
{
    public class ActorNotFoundException : NotFoundException
    {
        public ActorNotFoundException(int actorId) : base($"Actor with id:{actorId} not found.") { }
    }
}
