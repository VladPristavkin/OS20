using System.ComponentModel.DataAnnotations;

namespace FilmoSearchPortal.Application.DTO.User
{
    public record UserForReviewDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
    }
}
