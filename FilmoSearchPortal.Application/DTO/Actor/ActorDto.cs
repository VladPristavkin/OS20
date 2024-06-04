using FilmoSearchPortal.Application.DTO.Film;

namespace FilmoSearchPortal.Application.DTO.Actor
{
    public record ActorDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Biography { get; set; }

        public IEnumerable<FilmDto>? Films { get; set; }
    }
}
