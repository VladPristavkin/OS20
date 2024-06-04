using FilmoSearchPortal.Application.DTO.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FilmoSearchPortal.Application.CQRS.Commands.User
{
    public record RegisterUserCommand(UserForRegistrationDto UserForRegistration) : IRequest<IdentityResult>;
}
