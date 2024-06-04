using FilmoSearchPortal.Application.CQRS.Commands.User;
using FilmoSearchPortal.Application.DTO.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.WebApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto user)
        {
            if (user == null)
                return BadRequest("UserForRegistrationDto is null.");

            var result = await _sender.Send(new RegisterUserCommand(user));

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);

            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _sender.Send(new ValidateUserCommand(user)))
                return Unauthorized();

            return Ok(new { Token = await _sender.Send(new CreateTokenCommand(user)) });
        }
    }
}
