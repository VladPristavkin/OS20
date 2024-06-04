using FilmoSearchPortal.Application.DTO.User;
using MediatR;

namespace FilmoSearchPortal.Application.CQRS.Commands.User
{
    public record ValidateUserCommand(UserForAuthenticationDto userForAuthentication) : IRequest<bool>;
}
