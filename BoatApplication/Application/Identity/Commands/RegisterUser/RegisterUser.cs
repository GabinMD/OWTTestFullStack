using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Identity.Models;

namespace BoatApplication.Domain.Identity.Commands.RegisterUser
{
    public record RegisterUserCommand(string Name, string Password) : IRequest<AuthResponse>;

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public RegisterUserHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var createResult = await _identityService.CreateUserAsync(request.Name, request.Password);

            if (!createResult.result.Succeeded)
                return AuthResponse.Failure(createResult.result.Errors.ToList());

            Guard.Against.Null(createResult.user, nameof(createResult.user));

            var token = _tokenService.GenerateJwtToken(createResult.user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var tokenResult = await _tokenService.UpdateRefreshTokenAsync(createResult.user, refreshToken);

            if (!tokenResult.Succeeded)
                return AuthResponse.Failure(tokenResult.Errors.ToList());

            return AuthResponse.Success(token, refreshToken);
        }
    }

}
