using FilmoSearchPortal.Application.DTO.Actor;
using FilmoSearchPortal.Application.DTO.Director;
using FilmoSearchPortal.Application.DTO.Genre;
using FilmoSearchPortal.Application.DTO.Review;

namespace FilmoSearchPortal.Application.DTO.Film
{
    public record FilmDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int ReleaseYear { get; set; }
        public required int Duration { get; set; }
        public float Rating { get; set; }

        public DirectorForFilmDto? Director { get; set; }

        public IEnumerable<ActorForFilmDto>? Actors { get; set; }
        public IEnumerable<ReviewDto>? Reviews { get; set; }
        public IEnumerable<GenreForFilmDto>? Genres { get; set; }
    }
}
