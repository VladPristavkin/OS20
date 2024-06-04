using System.Text.Json.Serialization;

namespace FilmoSearchPortal.Application.DTO.Review
{
    public record ReviewForCreatingDto
    {
        [JsonIgnore]
        public string? UserId { get; set; }

        public required string Comment { get; set; }
        public required int Stars { get; set; }
    }
}
