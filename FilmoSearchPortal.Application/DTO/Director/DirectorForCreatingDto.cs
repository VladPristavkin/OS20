namespace FilmoSearchPortal.Application.DTO.Director
{
    public record DirectorForCreatingDto
    {
        public required string Name { get; set; }
        public string? Biography { get; set; }
    }
}
