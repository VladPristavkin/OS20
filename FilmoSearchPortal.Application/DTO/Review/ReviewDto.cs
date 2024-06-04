using FilmoSearchPortal.Application.DTO.User;

namespace FilmoSearchPortal.Application.DTO.Review
{
    public record ReviewDto
    {
        public required int Id { get; set; }

        public UserForReviewDto? User { get; set; }

        public required int Stars { get; set; }
        public required string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
