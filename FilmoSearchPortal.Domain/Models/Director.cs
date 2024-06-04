namespace FilmoSearchPortal.Domain.Models
{
    public class Director
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Biography { get; set; }

        public IEnumerable<Film>? Films { get; set; }
    }
}
