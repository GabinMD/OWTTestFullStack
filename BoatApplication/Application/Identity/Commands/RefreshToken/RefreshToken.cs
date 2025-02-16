using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Identity.Models;
using BoatApplication.Domain.Identity.DTOs;

namespace BoatApplication.Domain.Identity.Commands.RefreshToken
{

    public record RefreshTokenCommand(string token, string refreshToken) : IRequest<AuthResponse>;

    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var parseResult = _tokenService.ParseToken(request.token);

            if (!parseResult.result.Succeeded)
                return AuthResponse.Failure(parseResult.result.Errors.ToList());

            Guard.Against.Null(parseResult.principal, nameof(parseResult.principal));
            Guard.Against.Null(parseResult.user, nameof(parseResult.user));

            var validateResult = await _tokenService.ValidateRefreshTokenAsync(parseResult.user, request.refreshToken);
            if (!validateResult.Succeeded)
                return AuthResponse.Failure(validateResult.Errors.ToList());

            var newJwtToken = _tokenService.GenerateJwtToken(parseResult.user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var result = await _tokenService.UpdateRefreshTokenAsync(parseResult.user, newRefreshToken);

            if (!result.Succeeded)
                return AuthResponse.Failure(result.Errors.ToList());

            return AuthResponse.Success(newJwtToken, newRefreshToken);
        }
    }

}
