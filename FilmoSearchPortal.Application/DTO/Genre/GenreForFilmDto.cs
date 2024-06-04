﻿namespace FilmoSearchPortal.Application.DTO.Genre
{
    public record GenreForFilmDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
