using BoatApplication.Domain.Identity.DTOs;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BoatApplication.Domain.Identity.Interfaces;

namespace BoatApplication.Infrastructure.Identity.Extentions
{
    public static class UserExtentions
    {
        #region UserDto
        public static UserDto ToUserDto(this ClaimsPrincipal principal)
        {
            return new UserDto
            {
                Id = principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value,
                Name = principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Name).Value
            };
        }
        #endregion

        #region IUser
        public static List<Claim> Claims(this IUser user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id ?? ""),
                new Claim(JwtRegisteredClaimNames.Name, user.Name ?? "")
            };
        }
        #endregion
    }
}
