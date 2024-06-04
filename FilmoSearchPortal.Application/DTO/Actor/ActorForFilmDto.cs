namespace FilmoSearchPortal.Application.DTO.Actor
{
    public record ActorForFilmDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Biography { get; set; }
    }
}
