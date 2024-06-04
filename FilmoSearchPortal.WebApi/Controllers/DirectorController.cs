using FilmoSearchPortal.Application.CQRS.Commands.Director;
using FilmoSearchPortal.Application.CQRS.Queries.Director;
using FilmoSearchPortal.Application.DTO.Director;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorController : ControllerBase
    {
        private readonly ISender _sender;

        public DirectorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:int}", Name = "DirectorById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDirector(int id)
        {
            var director = await _sender.Send(new GetDirectorDetailsQuery(DirectorId: id, TrackChanges: false));

            return Ok(director);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDirector([FromBody] DirectorForCreatingDto directorForCreating)
        {
            if (directorForCreating == null)
                return BadRequest("DirectorForCreatingDto cannot be null.");

            var director = await _sender.Send(new CreateDirectorCommand(directorForCreating));

            return CreatedAtRoute("DirectorById", new { id = director.Id }, director);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            await _sender.Send(new DeleteDirectorCommand(DirectorId: id, TrackChanges: false));

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDirector(int id, [FromBody] DirectorForUpdateDto directorForUpdate)
        {
            if (directorForUpdate == null)
                return BadRequest("DirectorForUpdateDto cannot be null.");

            await _sender.Send(new UpdateDirectorCommand(DirectorId: id, directorForUpdate, TrackChanges: true));

            return NoContent();
        }
    }
}
