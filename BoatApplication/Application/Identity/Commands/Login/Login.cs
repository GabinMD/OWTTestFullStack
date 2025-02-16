using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Identity.Models;

namespace BoatApplication.Domain.Identity.Commands.Login
{
    public record LoginCommand(string Name, string Password) : IRequest<AuthResponse>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.PasswordSignInAsync(request.Name, request.Password);
            if (!result.result.Succeeded)
                return AuthResponse.Failure(result.result.Errors.ToList());

            Guard.Against.Null(result.user, nameof(result.user));

            var token = _tokenService.GenerateJwtToken(result.user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return AuthResponse.Success(token, refreshToken);
        }
    }

}
