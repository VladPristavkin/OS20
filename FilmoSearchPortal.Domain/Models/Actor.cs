namespace FilmoSearchPortal.Domain.Models
{
    public class Actor
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Biography { get; set; }

        public IEnumerable<Film>? Films { get; set; }
    }
}
