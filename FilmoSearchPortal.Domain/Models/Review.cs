namespace FilmoSearchPortal.Domain.Models
{
    public class Review
    {
        public required int Id { get; set; }
        public int FilmId { get; set; }
        public Film? Film { get; set; }

        public required string UserId { get; set; }
        public User? User { get; set; }

        public required int Stars { get; set; }
        public required string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
