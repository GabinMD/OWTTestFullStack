using AutoMapper;
using BoatApplication.Domain.Common.Interfaces;
using BoatApplication.Domain.Identity.DTOs;
using BoatApplication.Domain.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BoatApplication.Infrastructure.Identity;

public class User : IdentityUser, IUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string Name => UserName ?? "";

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
