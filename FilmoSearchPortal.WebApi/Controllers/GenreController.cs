using FilmoSearchPortal.Application.CQRS.Commands.Genre;
using FilmoSearchPortal.Application.CQRS.Queries.Genre;
using FilmoSearchPortal.Application.DTO.Genre;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly ISender _sender;

        public GenreController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _sender.Send(new GetGenreCollectionQuery(TrackChanges: false));

            return Ok(genres);
        }

        [HttpGet("{id:int}", Name = "GenreById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _sender.Send(new GetGenreDetailsQuery(GenreId: id, TrackChanges: false));

            return Ok(genre);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreForCreatingDto genreForCreating)
        {
            if (genreForCreating == null)
                return BadRequest("GenreForCreatingDto cannot be null.");

            var genre = await _sender.Send(new CreateGenreCommand(genreForCreating));

            return CreatedAtRoute("GenreById", new { id = genre.Id }, genre);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _sender.Send(new DeleteGenreCommand(GenreId: id, TrackChanges: false));

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreForUpdateDto genreForUpdate)
        {
            if (genreForUpdate == null)
                return BadRequest("GenreForUpdateDto cannot be null.");

            await _sender.Send(new UpdateGenreCommand(GenreId: id, genreForUpdate, TrackChanges: true));

            return NoContent();
        }
    }
}
