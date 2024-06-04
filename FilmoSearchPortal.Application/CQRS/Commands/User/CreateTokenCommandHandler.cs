using FilmoSearchPortal.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserEntity = FilmoSearchPortal.Domain.Models.User;

namespace FilmoSearchPortal.Application.CQRS.Commands.User
{
    public sealed class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, string>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandHandler(UserManager<UserEntity> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var signingCredentials = GetSigningCredentials();

            var user = await _userManager.FindByNameAsync(request.userForAuthentication.UserName) ??
                throw new AuthenticationException();

            var claims = await GetClaims(user);

            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication")
                .GetSection("EncryptionKey").Value ?? throw new AuthenticationException("EnctyptionKey is not exist."));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Authentication");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
