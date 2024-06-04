using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = FilmoSearchPortal.Domain.Models.User;

namespace FilmoSearchPortal.Application.CQRS.Commands.User
{
    public sealed class ValidateUserCommandHandler : IRequestHandler<ValidateUserCommand, bool>
    {
        private readonly UserManager<UserEntity> _userManager;

        public ValidateUserCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.userForAuthentication.UserName);

            var result = (user != null && await _userManager.CheckPasswordAsync(user, request.userForAuthentication.Password));

            return result;
        }
    }
}
