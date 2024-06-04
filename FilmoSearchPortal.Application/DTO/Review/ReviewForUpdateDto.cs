namespace FilmoSearchPortal.Application.DTO.Review
{
    public record ReviewForUpdateDto
    {
        public string? Comment { get; set; }
        public int? Stars { get; set; }
    }
}
