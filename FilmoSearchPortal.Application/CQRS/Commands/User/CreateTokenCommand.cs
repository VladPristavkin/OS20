using FilmoSearchPortal.Application.DTO.User;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.User
{
    public record CreateTokenCommand(UserForAuthenticationDto userForAuthentication) : IRequest<string>;
}
