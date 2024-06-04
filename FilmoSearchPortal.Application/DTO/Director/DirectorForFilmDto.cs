namespace FilmoSearchPortal.Application.DTO.Director
{
    public record DirectorForFilmDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Biography { get; set; }
    }
}
