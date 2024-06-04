using FilmoSearchPortal.Application.DTO.Film;

namespace FilmoSearchPortal.Application.DTO.Genre
{
    public record GenreDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public IEnumerable<FilmDto>? Films { get; set; }
    }
}
