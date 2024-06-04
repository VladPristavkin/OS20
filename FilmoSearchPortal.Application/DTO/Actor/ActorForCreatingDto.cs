namespace FilmoSearchPortal.Application.DTO.Actor
{
    public record ActorForCreatingDto
    {
        public required string Name { get; set; }
        public string? Biography { get; set; }
    }
}
