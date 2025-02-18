using BoatApplication.Domain.Identity.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BoatApplication.WebApp.Server.Services.Identity
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Name)?.Value;

    }
}
