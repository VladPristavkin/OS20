namespace FilmoSearchPortal.Domain.Models
{
    public class Genre
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public IEnumerable<Film>? Films { get; set; }
    }
}
