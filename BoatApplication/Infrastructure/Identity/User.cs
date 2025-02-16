using BoatApplication.Domain.Common.Interfaces;
using BoatApplication.Domain.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BoatApplication.Infrastructure.Identity;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    #region Converters
    public UserDto ToDto()
    {
        return new UserDto
        {
            Id = Id,
            Name = UserName
        };
    }
    #endregion
}
