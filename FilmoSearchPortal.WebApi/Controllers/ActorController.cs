using FilmoSearchPortal.Application.CQRS.Commands.Actor;
using FilmoSearchPortal.Application.CQRS.Queries.Actor;
using FilmoSearchPortal.Application.DTO.Actor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly ISender _sender;

        public ActorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetActors()
        {
            var actors = await _sender.Send(new GetActorCollectionQuery(TrackChanges: false));

            return Ok(actors);
        }

        [HttpGet("{id:int}", Name = "ActorById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await _sender.Send(new GetActorDetailsQuery(ActorId: id, TrackChanges: false));

            return Ok(actor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateActor([FromBody] ActorForCreatingDto actorForCreating)
        {
            if (actorForCreating == null)
                return BadRequest("ActorForCreatingDto cannot be null.");

            var actor = await _sender.Send(new CreateActorCommand(actorForCreating));

            return CreatedAtRoute("ActorById", new { id = actor.Id}, actor);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            await _sender.Send(new DeleteActorCommand(id, TrackChanges: false));

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] ActorForUpdateDto actorForUpdate)
        {
            if (actorForUpdate == null)
                return BadRequest("ActorForUpdateDto cannot be null.");

            await _sender.Send(new UpdateActorCommand(id, actorForUpdate, TrackChanges: true));

            return NoContent();
        }
    }
}
