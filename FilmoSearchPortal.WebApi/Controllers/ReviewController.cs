using FilmoSearchPortal.Application.CQRS.Commands.Review;
using FilmoSearchPortal.Application.CQRS.Queries.Review;
using FilmoSearchPortal.Application.DTO.Review;
using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/films/{filmId:int}/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly ISender _sender;

        public ReviewController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviews(int filmId)
        {
            var reviews = await _sender.Send(new GetReviewCollectionQuery(FilmId: filmId, TrackChanges: false));

            return Ok(reviews);
        }

        [HttpGet("{id:int}", Name = "ReviewById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReview(int filmId, int id)
        {
            var review = await _sender.Send(new GetReviewDetailsQuery(FilmId: filmId, ReviewId: id, TrackChanges: false));

            return Ok(review);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CreateReview(int filmId, [FromBody] ReviewForCreatingDto reviewForCreating)
        {
            if (reviewForCreating == null)
                return BadRequest("ReviewForCreatingDto cannot be null.");

            reviewForCreating.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = await _sender.Send(new CreateReviewCommand(FilmId: filmId, reviewForCreating));

            return CreatedAtRoute("ReviewById", new { filmId = filmId, id = review.Id }, review);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _sender.Send(new DeleteReviewCommand(ReviewId: id, TrackChanges: false));

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewForUpdateDto reviewForUpdate)
        {
            if (reviewForUpdate == null)
                return BadRequest("ReviewForUpdateDto cannot be null.");

            await _sender.Send(new UpdateReviewCommand(ReviewId: id, reviewForUpdate, TrackChanges: true));

            return NoContent();
        }
    }
}
