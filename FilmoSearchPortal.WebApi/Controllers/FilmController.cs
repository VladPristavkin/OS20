using FilmoSearchPortal.Application.CQRS.Commands.Film;
using FilmoSearchPortal.Application.CQRS.Queries.Film;
using FilmoSearchPortal.Application.DTO.Film;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/films")]
    public class FilmController : ControllerBase
    {
        private readonly ISender _sender;

        public FilmController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _sender.Send(new GetFilmCollectionQuery(TrackChanges: false));

            return Ok(films);
        }

        [HttpGet("{id:int}", Name = "FilmById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilm(int id)
        {
            var film = await _sender.Send(new GetFilmDetailsQuery(FilmId: id, TrackChanges: false));

            return Ok(film);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFilm([FromBody] FilmForCreatingDto filmForCreating)
        {
            if (filmForCreating == null)
                return BadRequest("FilmForCreatingDto cannot be null.");

            var film = await _sender.Send(new CreateFilmCommand(filmForCreating));

            return CreatedAtRoute("FilmById", new { id = film.Id}, film);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            await _sender.Send(new DeleteFilmCommand(FilmId: id, TrackChanges: false));

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] FilmForUpdateDto filmForUpdate)
        {
            if (filmForUpdate == null)
                return BadRequest("FilmForUpdateDto cannot be null.");

            await _sender.Send(new UpdateFilmCommand(FilmId: id, filmForUpdate, TrackChanges: true));

            return NoContent();
        }
    }
}
