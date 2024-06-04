namespace FilmoSearchPortal.Application.DTO.Film
{
    public record FilmForCreatingDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int ReleaseYear { get; set; }
        public required int Duration { get; set; }
        public float Rating { get; set; }

        public int DirectorId { get; set; }

        public IEnumerable<int>? ActorIds { get; set; }
        public IEnumerable<int>? GenresIds { get; set; }
    }
}
