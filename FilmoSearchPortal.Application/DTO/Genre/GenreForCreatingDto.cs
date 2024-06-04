namespace FilmoSearchPortal.Application.DTO.Genre
{
    public record GenreForCreatingDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
