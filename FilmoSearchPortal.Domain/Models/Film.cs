namespace FilmoSearchPortal.Domain.Models
{
    public class Film
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int ReleaseYear { get; set; }
        public required int Duration { get; set; }
        public float Rating { get; set; }

        public int DirectorId { get; set; }
        public Director? Director { get; set; }

        public IEnumerable<Actor>? Actors { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
    }
}
